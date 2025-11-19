using MediatR;

namespace NutritionService.Features.Meals.GetMealDetails
{
    public static class GetMealDetailsEndpoint
    {
        public static void MapGetMealDetailsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/nutrition/meals/{id:int}",
                async (int id, IMediator mediator) =>
                {
                    var query = new GetMealDetailsQuery { Id = id };
                    var result = await mediator.Send(query);
                    return Results.Ok(result);
                });
        }
    }
}
