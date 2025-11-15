using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.Exercises.GetExerciseDetails
{
    public static class Endpoints
    {
        public static void MapGetExerciseDetailsEndpoint(this WebApplication app)
        {
            app.MapGet("/exercises/{id}", async (int id, [FromServices] IMediator mediator) =>
            {
                var query = new GetExerciseDetailsQuery(id);
                var result = await mediator.Send(query);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });
        }
    }
}
