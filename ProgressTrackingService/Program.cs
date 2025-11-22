
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgressTrackingService.Api;
using ProgressTrackingService.Data;

namespace ProgressTrackingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DbContext
            builder.Services.AddDbContext<ProgressDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // MediatR
            builder.Services.AddMediatR(typeof(Program).Assembly);

            // MemoryCache
            builder.Services.AddMemoryCache();
            builder.Services.AddHttpClient();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Map Minimal API endpoints
            app.MapProgressEndpoints();

            app.Run();
        }
    }
}
