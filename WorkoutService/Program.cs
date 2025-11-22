using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;
using WorkoutService.Domain.Entities;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features;
using WorkoutService.Infrastructure;
using WorkoutService.Infrastructure.Data;
using WorkoutService.Infrastructure.UnitOfWork;

// Change Main signature to be async
public class Program
{
    public static async Task Main(string[] args) // Changed to async Task
    {
        // Serilog setup
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "WorkoutService")
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} | {CorrelationId} | {Message:lj}{NewLine}{Exception}", theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Literate)
            .WriteTo.File("logs/WorkoutService-.log", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {SourceContext} | {CorrelationId} | {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        try
        {
            Log.Information("Starting WorkoutService");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();

            var config = builder.Configuration;

            // 1. Add Services to the container

            // Add DBContext for SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                if (builder.Environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging(true);
                    options.EnableDetailedErrors(true);
                }
            });

            // Register Unit of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Generic Repositories for all classes inheriting from BaseEntity
            var entityTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseEntity)))
                .ToList();

            foreach (var entityType in entityTypes)
            {
                var interfaceType = typeof(IBaseRepository<>).MakeGenericType(entityType);
                var implementationType = typeof(BaseRepository<>).MakeGenericType(entityType);
                builder.Services.AddScoped(interfaceType, implementationType);
            }

            Log.Information("Registered {Count} generic repositories for entities: {Entities}",
                entityTypes.Count,
                string.Join(", ", entityTypes.Select(t => t.Name)));

            // Add MediatR
            builder.Services.AddMediatR(typeof(Program).Assembly);

            // Add Mapster
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
            builder.Services.AddSingleton(typeAdapterConfig);

            // ---------------------------------------------------------
            // ✅ 1. إضافة خدمة الـ CORS (مهم جداً عشان المتصفح)
            // ---------------------------------------------------------
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials());
            });

            // Add Authentication (validates JWT tokens)
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                };
            });

            builder.Services.AddAuthorization();

            // Add Swagger/OpenAPI with JWT support
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkoutService API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field (e.g., 'Bearer {token}')",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            // 2. Configure the HTTP request pipeline

            // --- Database Migration and Seeding ---
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    Log.Information("📊 Starting database migration...");
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    // Apply pending migrations
                    await context.Database.MigrateAsync();
                    Log.Information("✅ Database migration completed.");

                    // Seed the database
                    Log.Information("🌱 Starting database seeding...");
                    await DatabaseSeeder.SeedAsync(services);
                    Log.Information("🌱 Database seeding completed successfully.");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "❌ An error occurred while migrating or seeding the database.");
                    if (app.Environment.IsDevelopment())
                    {
                        // In Docker, sometimes it's better NOT to throw here to keep the container alive for inspection
                        throw;
                    }
                }
            }
            // --- END OF NEW BLOCK ---

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // ---------------------------------------------------------
            // ✅ 2. تفعيل الـ CORS (لازم يكون قبل الـ Authentication)
            // ---------------------------------------------------------
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            // 3. Map all endpoints
            app.MapAllEndpoints();

            // 4. Run the application
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application failed to start");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}