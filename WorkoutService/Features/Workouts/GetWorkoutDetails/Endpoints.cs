using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Features.Shared;
using WorkoutService.Features.Workouts.GetWorkoutDetails.ViewModels;

namespace WorkoutService.Features.Workouts.GetWorkoutDetails
{
    public static class Endpoints
    {
        public static void MapGetWorkoutDetailsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/workouts/{id}", async (
                [FromRoute] int id,
                [FromServices] IMediator mediator) =>
            {
                var query = new GetWorkoutDetailsQuery(id);
                var result = await mediator.Send(query);

                if (!result.IsSuccess)
                {
                    var response = EndpointResponse<object>.NotFoundResponse(result.Message);
                    return Results.Json(response, statusCode: response.StatusCode);
                }

                var success = EndpointResponse<WorkoutDetailsViewModel>.SuccessResponse(
                    result.Data!,
                    result.Message
                );

                return Results.Json(success, statusCode: success.StatusCode);
            });


        }
    }
}
