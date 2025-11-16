using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Features.Shared;
using WorkoutService.Features.Workouts.StartWorkoutSession.Dtos;
using WorkoutService.Features.Workouts.StartWorkoutSession.ViewModels;

namespace WorkoutService.Features.Workouts.StartWorkoutSession
{
    public static class Endpoints
    {
        public static void MapStartWorkoutSessionEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/workouts/{id}/start", async (
                [FromServices] IMediator mediator,
                [FromRoute] int id,
                [FromBody] StartWorkoutSessionDto dto) =>
            {
                var command = new StartWorkoutSessionCommand(id, dto);
                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(new EndpointResponse<object>(
                        null,
                        result.Message,
                        false,
                        400,
                        new List<string> { result.Message },
                        DateTime.UtcNow
                    ));
                }

                return Results.Ok(new EndpointResponse<WorkoutSessionViewModel>(
                    result.Data,
                    result.Message,
                    true,
                    200,
                    null,
                    DateTime.UtcNow
                ));
            });
        }
    }
}
