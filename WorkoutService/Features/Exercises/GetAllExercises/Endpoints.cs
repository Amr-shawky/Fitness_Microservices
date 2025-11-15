using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.Exercises.GetAllExercises
{
    public static class Endpoints
    {
        public static void MapGetAllExercisesEndpoint(this WebApplication app)
        {
            app.MapGet("/exercises", async ([FromServices] IMediator mediator) =>
            {
                var query = new GetAllExercisesQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }
}
