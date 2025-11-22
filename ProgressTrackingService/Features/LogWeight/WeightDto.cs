namespace ProgressTrackingService.Features.LogWeight
{
    public class WeightDto
    {
        public Guid Id { get; set; }
        public decimal WeightKg { get; set; }
        public DateTimeOffset LoggedAt { get; set; }
    }
}
