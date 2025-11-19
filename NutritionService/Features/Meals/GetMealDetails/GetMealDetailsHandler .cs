using MediatR;
using Microsoft.EntityFrameworkCore;
using NutritionService.Features.Meals.GetMealDetails.DTOs;
using NutritionService.Features.Shared;
using NutritionService.Infrastructure.Data;

namespace NutritionService.Features.Meals.GetMealDetails
{
    public class GetMealDetailsHandler
          : IRequestHandler<GetMealDetailsQuery, EndpointResponse<MealDetailsDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetMealDetailsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EndpointResponse<MealDetailsDto>> Handle(
            GetMealDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var meal = await _context.meals
                .Where(m => m.Id == request.Id && !m.IsDeleted)
                .Select(m => new MealDetailsDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    MealType = m.mealType.ToString(),
                    ImageUrl = m.ImageUrl,
                    PrepTime = m.PrepTimeInMinutes,
                    Difficulty = m.Difficulty,
                    IsPremium = m.IsPremium,
                    Servings = 1,

                    Nutrition = new NutritionDetailsDto
                    {
                        Calories = m.NutritionFacts.Calories,
                        Protein = m.NutritionFacts.Protein,
                        Carbs = m.NutritionFacts.Carbs,
                        Fats = m.NutritionFacts.Fats,
                        Fiber = m.NutritionFacts.Fiber,
                        Sugar = 5 
                    },
                    Ingredients = m.MealIngredients
                        .Select(i => new IngredientDto
                        {
                            Name = i.Ingredient.Name,
                            Amount = i.Amount
                        }).ToList(),

                    Tags = new List<string> { "high-protein", "quick" },
                    Allergens = new List<string> { "eggs", "gluten" },

                    Variations = new VariationDto
                    {
                        Beginner = new VariationLevelDto
                        {
                            Name = "Simplified",
                            Modifications = new() { "Use pre-chopped vegetables" },
                            Calories = m.NutritionFacts.Calories - 30
                        },
                        Intermediate = new VariationLevelDto
                        {
                            Name = "Standard",
                            Modifications = new() { "As described" },
                            Calories = m.NutritionFacts.Calories
                        },
                        Advanced = new VariationLevelDto
                        {
                            Name = "Enhanced",
                            Modifications = new() { "Add avocado", "Use egg whites only" },
                            Calories = m.NutritionFacts.Calories + 70
                        }
                    }
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (meal == null)
            {
                return EndpointResponse<MealDetailsDto>.NotFoundResponse("Meal not found");
            }

            return EndpointResponse<MealDetailsDto>.SuccessResponse(
                meal,
                "Meal details fetched successfully"
                );
        }
    }
}