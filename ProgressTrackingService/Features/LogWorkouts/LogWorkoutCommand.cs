using MediatR;
using ProgressTrackingService.Shared;

namespace ProgressTrackingService.Features.LogWorkout
{
    public record LogWorkoutCommand(
       Guid UserId,
       Guid SessionId,
       Guid? WorkoutId,
       int DurationMinutes,
       int CaloriesBurned,
       int Rating,
       DateTimeOffset PerformedAt,
       string ClientRequestId // for idempotency
   ) : IRequest<EndpointResponse<WorkoutLogDto>>;
}
