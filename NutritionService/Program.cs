using MediatR;
using Microsoft.EntityFrameworkCore;
using NutritionService.Domain.Interfaces;
using NutritionService.Features.Meals.GetMealDetails;
using NutritionService.Features.Meals.GetMealRecommendations;
using NutritionService.Infrastructure.Data;
using NutritionService.Infrastructure.Repositorys;

namespace NutritionService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region services to the container.
            builder.Services.AddAuthorization();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(typeof(Program).Assembly);
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion
            var app = builder.Build();

            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };


            app.MapGetMealRecommendationsEndpoint();
            app.MapGetMealDetailsEndpoint();

         
            #endregion
                app.Run();
        }
    }
}
