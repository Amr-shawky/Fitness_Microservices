using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.Workouts.GetWorkoutsByCategory
{
    public static class Endpoints
    {
        public static void MapGetWorkoutsByCategoryEndpoint(this WebApplication app)
        {
            app.MapGet("/workouts/category/{category}", async (string category, [FromServices] IMediator mediator) =>
            {
                var query = new GetWorkoutsByCategoryQuery(category);
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }
}
