using AuthenticationService.Contarcts;
using AuthenticationService.Data.Seed;
using AuthenticationService.Features.Auth.Login.AuthenticationService.Features.Auth.Login;
using AuthenticationService.Features.Auth.Register;
using AuthenticationService.Features.Auth.UpdateUserProfile;
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using AuthenticationService.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Online_Exam_System.Data;
using Online_Exam_System.Repositories;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace AuthenticationService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuperFitnessApp Auth API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Database
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SuperFitnessAppAuthContext>(options =>
                options.UseSqlServer(connectionString, opts =>
                    opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<SuperFitnessAppAuthContext>()
            .AddDefaultTokenProviders();

            // JWT
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            if (!string.IsNullOrEmpty(secretKey))
            {
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                })
                .AddJwtBearer("JwtBearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };

                    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new
                            {
                                statusCode = 401,
                                message = "You are not authenticated. Please provide a valid token."
                            });
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new
                            {
                                statusCode = 403,
                                message = "You are not authorized to access this resource."
                            });
                            return context.Response.WriteAsync(result);
                        }
                    };
                });
            }

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true) // allow any origin
                        .AllowCredentials());
            });

            // DI
            builder.Services.AddScoped<IImageHelper, ImageHelper>();
            builder.Services.AddScoped<UpdateUserProfileOrchestrator>();
            builder.Services.AddScoped<ITokenService, JwtService>();
            builder.Services.AddScoped<IMailKitEmailService, MailKitEmailService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();
            builder.Services.AddValidatorsFromAssembly(typeof(RegisterCommandValidator).Assembly);
            builder.Services.AddValidatorsFromAssembly(typeof(LoginValidator).Assembly);

            // ?? Add MediatR (FIX)
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            var app = builder.Build();

            #region Database Seeding
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SuperFitnessAppAuthContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                    await context.Database.MigrateAsync();
                    await IdentitySeeder.SeedIdentityAsync(roleManager, userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error occurred while seeding initial data");
                }
            }
            #endregion

            // Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseMiddleware<Middlewares.GlobalExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
