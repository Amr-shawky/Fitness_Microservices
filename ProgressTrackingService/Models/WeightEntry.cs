namespace ProgressTrackingService.Models
{
    public class WeightEntry :BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal WeightKg { get; set; }
        public DateTimeOffset LoggedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
