using MediatR;
using Microsoft.Extensions.Caching.Memory;
using NutritionService.Features.Meals.Filters;
using NutritionService.Features.Shared;
using NutritionService.Infrastructure.Data;

namespace NutritionService.Features.Meals.GetMealRecommendations
{
    public class GetMealRecommendationsHandlers
        : IRequestHandler<GetMealRecommendationsQuery, PaginatedResult<MealRecommendationDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public GetMealRecommendationsHandlers(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<PaginatedResult<MealRecommendationDto>> Handle(
            GetMealRecommendationsQuery request,
            CancellationToken cancellationToken)
        {
            // ⚡ Generate unique cache key based on filters + paging
            var cacheKey =
                $"meal_recommendations_" +
                $"{request.Page}_" +
                $"{request.PageSize}_" +
                $"{request.MealType}_" +
                $"{request.MaxCalories}_" +
                $"{request.MinProtein}";

            // ✔ 1) Try get from cache
            if (_cache.TryGetValue(cacheKey, out PaginatedResult<MealRecommendationDto> cachedResult))
            {
                return cachedResult;
            }

            // ✔ 2) Build Query
            var query = _context.meals
                .Where(m => !m.IsDeleted)
                .AsQueryable();

            query = MealFilter.ApplyFilters(
                query,
                request.MealType,
                request.MaxCalories,
                request.MinProtein
            );

            var projectedQuery = query.Select(m => new MealRecommendationDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                MealType = m.mealType.ToString(),

                Calories = m.NutritionFacts.Calories,
                Protein = m.NutritionFacts.Protein,
                Carbs = m.NutritionFacts.Carbs,
                Fats = m.NutritionFacts.Fats,

                PrepTime = m.PrepTimeInMinutes,
                ImageUrl = m.ImageUrl,
                Difficulty = m.Difficulty,
                IsPremium = m.IsPremium
            });

            // ✔ 3) Execute & paginate
            var result = await projectedQuery.ToPaginatedResultAsync(
                request.Page,
                request.PageSize,
                cancellationToken
            );

            // ✔ 4) Save to cache for 5 minutes
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            _cache.Set(cacheKey, result, cacheOptions);

            return result;
        }
    }
}
