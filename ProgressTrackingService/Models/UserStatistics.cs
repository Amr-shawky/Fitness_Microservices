namespace ProgressTrackingService.Models
{
    public class UserStatistics : BaseEntity
    {
        public Guid UserId { get; set; }
        public int TotalWorkouts { get; set; }
        public int TotalCaloriesBurned { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public decimal StartingWeight { get; set; }
        public decimal CurrentWeight { get; set; }
        public DateTimeOffset LastWorkoutAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
