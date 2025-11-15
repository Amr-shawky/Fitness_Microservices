using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.Workouts.CreateWorkout
{
    public static class Endpoints
    {
        public static void MapCreateWorkoutEndpoint(this WebApplication app)
        {
            app.MapPost("/workouts", async ([FromBody] CreateWorkoutDto dto, [FromServices] IMediator mediator) =>
            {
                var command = new CreateWorkoutCommand(dto);
                var result = await mediator.Send(command);
                return Results.Created($"/workouts/{result.Id}", result);
            });
        }
    }
}
