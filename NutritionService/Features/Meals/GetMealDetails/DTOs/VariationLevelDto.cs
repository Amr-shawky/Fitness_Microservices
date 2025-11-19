namespace NutritionService.Features.Meals.GetMealDetails.DTOs
{
    public class VariationLevelDto
    {
        public string Name { get; set; } = default!;
        public List<string> Modifications { get; set; } = new();
        public int Calories { get; set; }
    }
}
