using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Features.Shared;

namespace WorkoutService.Features.Workouts.GetAllWorkouts
{
    public static class Endpoints
    {
        public static void MapGetAllWorkoutsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/workouts", async (
                [FromServices] IMediator mediator,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 20,
                [FromQuery] string? category = null,
                [FromQuery] string? difficulty = null,
                [FromQuery] int? duration = null,
                [FromQuery] string? search = null) =>
            {
                var query = new GetAllWorkoutsQuery(page, pageSize, category, difficulty, duration, search);
                var result = await mediator.Send(query);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<object>.ErrorResponse(
                        message: "Failed to fetch workouts",
                        errors: new List<string> { result.Message }
                    ));
                }

                return Results.Ok(EndpointResponse<object>.SuccessResponse(
                    data: result.Data,
                    message: "Workouts fetched successfully"
                ));
            });
        }
    }
}
