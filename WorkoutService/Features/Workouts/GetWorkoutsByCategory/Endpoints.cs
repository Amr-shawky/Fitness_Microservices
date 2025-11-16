using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Features.Shared;
using WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels;

namespace WorkoutService.Features.Workouts.GetWorkoutsByCategory
{
    public static class Endpoints
    {
        public static void MapGetWorkoutsByCategoryEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/workouts/category/{categoryName}", async (
                [FromServices] IMediator mediator,
                [FromRoute] string categoryName,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 20,
                [FromQuery] string? difficulty = null) =>
            {
                var query = new GetWorkoutsByCategoryQuery(categoryName, page, pageSize, difficulty);
                var result = await mediator.Send(query);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(
                        new EndpointResponse<object>(
                            null,
                            result.Message,
                            false,
                            400,
                            new List<string> { result.Message },
                            DateTime.UtcNow
                        )
                    );
                }

                return Results.Ok(
                    new EndpointResponse<PaginatedResult<WorkoutViewModel>>(
                        result.Data,
                        result.Message,
                        true,
                        200,
                        null,
                        DateTime.UtcNow
                    )
                );
            });

        }
    }
}
