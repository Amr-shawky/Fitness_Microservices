using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans
{
    public static class Endpoints
    {
        public static void MapGetAllWorkoutPlansEndpoint(this WebApplication app)
        {
            app.MapGet("/workout-plans", async ([FromServices] IMediator mediator) =>
            {
                var query = new GetAllWorkoutPlansQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }
}
