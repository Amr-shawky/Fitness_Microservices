namespace NutritionService.Features.Meals.GetMealDetails.DTOs
{
    public class MealDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string MealType { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public int PrepTime { get; set; }
        public string Difficulty { get; set; } = default!;
        public bool IsPremium { get; set; }
        public int Servings { get; set; }

        public NutritionDetailsDto Nutrition { get; set; } = default!;
        public List<IngredientDto> Ingredients { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public List<string> Allergens { get; set; } = new();
        public VariationDto Variations { get; set; } = default!;
    }
}
