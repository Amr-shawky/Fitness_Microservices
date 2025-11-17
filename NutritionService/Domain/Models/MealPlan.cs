namespace NutritionService.Domain.Models
{
    public class MealPlan : BaseEntity 
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int CalorieTarget { get; set; }

        #region Relationships
        public ICollection<Meal> Meals { get; set; }
        #endregion
    }
}
