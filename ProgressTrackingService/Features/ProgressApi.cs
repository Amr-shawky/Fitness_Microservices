using MediatR;

using ProgressTrackingService.Features.LogWeight;
using ProgressTrackingService.Features.LogWorkout;
using ProgressTrackingService.Features.LogWorkouts;
using ProgressTrackingService.Features.Progress;

namespace ProgressTrackingService.Api;

public static class ProgressApi
{
    public static void MapProgressEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/progress");

        group.MapPost("/workouts", async (
            LogWorkoutRequest req,
            ISender mediator) =>
        {
            var cmd = new LogWorkoutCommand(
                req.UserId,
                req.SessionId,
                req.WorkoutId,
                req.DurationMinutes,
                req.CaloriesBurned,
                req.Rating,
                req.PerformedAt,
                req.ClientRequestId
            );

            var res = await mediator.Send(cmd);
            return res.Success ? Results.Created("", res) : Results.BadRequest(res);
        })
        .WithName("LogWorkout")
        .WithOpenApi();

        group.MapPost("/weight", async (
            LogWeightRequest req,
            ISender mediator) =>
        {
            var cmd = new LogWeightCommand(
                req.UserId,
                req.WeightKg,
                req.LoggedAt,
                req.ClientRequestId
            );

            var res = await mediator.Send(cmd);
            return res.Success ? Results.Created("", res) : Results.BadRequest(res);
        })
        .WithName("LogWeight")
        .WithOpenApi();

        group.MapGet("", async (
            Guid userId,
            string period,
            ISender mediator) =>
        {
            var q = new GetUserProgressQuery(userId, period);
            var res = await mediator.Send(q);
            return res.Success ? Results.Ok(res) : Results.NotFound(res);
        })
        .WithName("GetProgress")
        .WithOpenApi();
    }
}

