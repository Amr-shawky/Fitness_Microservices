namespace NutritionService.Domain.Models
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; } = default!;
        #region Relationships
        public ICollection<MealIngredient> MealIngredients { get; set; }
        #endregion
    }
}
