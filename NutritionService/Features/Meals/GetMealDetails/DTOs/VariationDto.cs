namespace NutritionService.Features.Meals.GetMealDetails.DTOs
{
    public class VariationDto
    {
        public VariationLevelDto Beginner { get; set; } = default!;
        public VariationLevelDto Intermediate { get; set; } = default!;
        public VariationLevelDto Advanced { get; set; } = default!;
    }
}
