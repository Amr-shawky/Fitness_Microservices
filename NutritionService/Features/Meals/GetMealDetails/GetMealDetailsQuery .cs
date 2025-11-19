using MediatR;
using NutritionService.Features.Meals.GetMealDetails.DTOs;
using NutritionService.Features.Shared;

namespace NutritionService.Features.Meals.GetMealDetails
{
    public class GetMealDetailsQuery : IRequest<EndpointResponse<MealDetailsDto>>
    {
        public int Id { get; set; }
    }
}
