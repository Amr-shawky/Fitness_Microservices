using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.Workouts.StartWorkoutSession
{
    public static class Endpoints
    {
        public static void MapStartWorkoutSessionEndpoint(this WebApplication app)
        {
            app.MapPost("/workouts/start-session", async ([FromBody] StartWorkoutSessionDto dto, [FromServices] IMediator mediator) =>
            {
                var command = new StartWorkoutSessionCommand(dto);
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
        }
    }
}
