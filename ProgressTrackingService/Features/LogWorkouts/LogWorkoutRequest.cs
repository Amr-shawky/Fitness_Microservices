namespace ProgressTrackingService.Features.LogWorkouts
{
    public record LogWorkoutRequest(Guid UserId, Guid SessionId, Guid? WorkoutId, int DurationMinutes, int CaloriesBurned, int Rating, DateTimeOffset PerformedAt, string ClientRequestId);

}
