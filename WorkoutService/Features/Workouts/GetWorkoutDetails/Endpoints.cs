using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.Workouts.GetWorkoutDetails
{
    public static class Endpoints
    {
        public static void MapGetWorkoutDetailsEndpoint(this WebApplication app)
        {
            app.MapGet("/workouts/{id}", async (int id, [FromServices] IMediator mediator) =>
            {
                var query = new GetWorkoutDetailsQuery(id);
                var result = await mediator.Send(query);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });
        }
    }
}
