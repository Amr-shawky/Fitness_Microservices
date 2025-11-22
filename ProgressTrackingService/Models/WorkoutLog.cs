namespace ProgressTrackingService.Models
{
    public class WorkoutLog :BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid? WorkoutId { get; set; }
        public Guid SessionId { get; set; }
        public int DurationMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset PerformedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool IsDeleted { get; set; } = false;

    }
}
