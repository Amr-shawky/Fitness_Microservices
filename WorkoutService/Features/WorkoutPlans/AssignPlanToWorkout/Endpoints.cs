using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutService.Features.WorkoutPlans.AssignPlanToWorkout
{
    public static class Endpoints
    {
        public static void MapAssignPlanToWorkoutEndpoint(this WebApplication app)
        {
            app.MapPost("/workout-plans/{planId}/assign-to/{workoutId}", async (int planId, int workoutId, [FromServices] IMediator mediator) =>
            {
                var command = new AssignPlanToWorkoutCommand(planId, workoutId);
                await mediator.Send(command);
                return Results.Ok();
            });
        }
    }
}
