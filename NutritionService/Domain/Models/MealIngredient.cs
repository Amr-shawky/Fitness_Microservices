namespace NutritionService.Domain.Models
{
    public class MealIngredient :BaseEntity
    {
        public string Amount { get; set; } = default!;

        #region Relationships
        public int MealId { get; set; }
        public Meal Meal { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        #endregion

    }
}
