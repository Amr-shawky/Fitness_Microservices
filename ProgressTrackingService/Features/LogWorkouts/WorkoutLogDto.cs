namespace ProgressTrackingService.Features.LogWorkout
{
    public class WorkoutLogDto{
        public Guid Id { get; set; }
        public int DurationMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public DateTimeOffset PerformedAt { get; set; }
  }
}
