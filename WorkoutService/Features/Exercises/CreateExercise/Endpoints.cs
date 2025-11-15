using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.Exercises.CreateExercise
{
    public static class Endpoints
    {
        public static void MapCreateExerciseEndpoint(this WebApplication app)
        {
            app.MapPost("/exercises", async ([FromBody] CreateExerciseDto dto, [FromServices] IMediator mediator) =>
            {
                var command = new CreateExerciseCommand(dto);
                var result = await mediator.Send(command);
                return Results.Created($"/exercises/{result.Id}", result);
            });
        }
    }
}
