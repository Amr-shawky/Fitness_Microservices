//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using WorkoutService.Features.Shared;
//using WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels;

//namespace WorkoutService.Features.Workouts.GetAllWorkouts
//{
//    public static class Endpoints
//    {
//        public static void MapGetAllWorkoutsEndpoint(this WebApplication app)
//        {
//            app.MapGet("/api/v1/workouts", async (
//                [FromServices] IMediator mediator,
//                [FromQuery] int page = 1,
//                [FromQuery] int pageSize = 20,
//                [FromQuery] string? category = null,
//                [FromQuery] string? difficulty = null,
//                [FromQuery] int? duration = null,
//                [FromQuery] string? search = null) =>
//            {
//                var query = new GetAllWorkoutsQuery(page, pageSize, category, difficulty, duration, search);
//                var result = await mediator.Send(query);

//                if (!result.IsSuccess)
//                {
//                    return Results.BadRequest(new EndpointResponse<object>(
//                        false,
//                        result.Message,
//                        null,
//                        new List<string> { result.Message },
//                        400,
//                        DateTime.UtcNow
//                    ));
//                }

//                return Results.Ok(new EndpointResponse<PaginatedResult<WorkoutViewModel>>(
//                    true,
//                    result.Message,
//                    result.Data,
//                    null,
//                    200,
//                    DateTime.UtcNow
//                ));
//            });
//        }
//    }
//}
