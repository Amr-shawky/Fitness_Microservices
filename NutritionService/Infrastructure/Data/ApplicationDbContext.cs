using Microsoft.EntityFrameworkCore;
using NutritionService.Domain.Models;

namespace NutritionService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Ingredient> ingredients { get; set; }
        public DbSet<Meal> meals { get; set; }
        public DbSet<MealIngredient> mealIngredients { get; set; }
        public DbSet<MealPlan> mealPlans { get; set; }
        public DbSet<NutritionFact> nutritionFacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
