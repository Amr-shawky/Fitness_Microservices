using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.WorkoutPlans.GetWorkoutPlanDetails
{
    public static class Endpoints
    {
        public static void MapGetWorkoutPlanDetailsEndpoint(this WebApplication app)
        {
            app.MapGet("/workout-plans/{id}", async (int id, [FromServices] IMediator mediator) =>
            {
                var query = new GetWorkoutPlanDetailsQuery(id);
                var result = await mediator.Send(query);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });
        }
    }
}
