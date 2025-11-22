using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NutritionService.Features.Meals.GetMealDetails.DTOs;
using NutritionService.Features.Shared;
using NutritionService.Infrastructure.Data;

namespace NutritionService.Features.Meals.GetMealDetails
{
    public class GetMealDetailsHandler
          : IRequestHandler<GetMealDetailsQuery, EndpointResponse<MealDetailsDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public GetMealDetailsHandler(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<EndpointResponse<MealDetailsDto>> Handle(
            GetMealDetailsQuery request,
            CancellationToken cancellationToken)
        {
            string cacheKey = $"meal_details_{request.Id}";

            // ✔ 1) Try Get From Cache
            if (_cache.TryGetValue(cacheKey, out MealDetailsDto cachedMeal))
            {
                return EndpointResponse<MealDetailsDto>.SuccessResponse(
                    cachedMeal,
                    "Meal details fetched successfully (from cache)"
                );
            }

            // ✔ 2) Fetch From DB
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

            // ✔ 3) Set Cache For 10 Minutes
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            _cache.Set(cacheKey, meal, cacheOptions);

            return EndpointResponse<MealDetailsDto>.SuccessResponse(
                meal,
                "Meal details fetched successfully"
            );
        }
    }
}
