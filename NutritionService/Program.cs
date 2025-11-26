using MediatR;
using Microsoft.EntityFrameworkCore;
using NutritionService.Domain.Interfaces;
using NutritionService.Features.Meals.GetMealDetails;
using NutritionService.Features.Meals.GetMealRecommendations;
using NutritionService.Infrastructure.Data;
using NutritionService.Infrastructure.Repositorys;
using System.Reflection;

namespace NutritionService
{
    public class Program
    {
        // ✅ 1. Changed to async Task to support async migration
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // --- Services Configuration ---

            // ✅ 2. Add CORS Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());
            });

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(typeof(Program).Assembly);
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Database Context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // --- Pipeline Configuration ---

            // ✅ 3. Database Migration & Seeding Block (Added this!)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    Console.WriteLine("📊 [Nutrition] Starting database migration...");
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    // This creates the NutritionDB database automatically
                    await context.Database.MigrateAsync();

                    Console.WriteLine("✅ [Nutrition] Database migration completed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ [Nutrition] Error during migration: {ex.Message}");
                    // We don't throw here to keep the service alive for debugging
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // ✅ 4. Use CORS before Authorization
            app.UseCors("AllowAll");

            app.UseAuthorization();

            // Endpoint Mapping
            app.MapGetMealRecommendationsEndpoint();
            app.MapGetMealDetailsEndpoint();

            // ✅ 5. Run Async
            await app.RunAsync();
        }
    }
}