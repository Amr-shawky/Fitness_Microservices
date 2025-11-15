using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.Workouts.GetAllWorkouts
{
    public static class Endpoints
    {
        public static void MapGetAllWorkoutsEndpoint(this WebApplication app)
        {
            app.MapGet("/workouts", async ([FromServices] IMediator mediator) =>
            {
                var query = new GetAllWorkoutsQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }
}
