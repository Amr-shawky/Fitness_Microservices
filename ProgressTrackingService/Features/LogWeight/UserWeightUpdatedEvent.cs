namespace ProgressTrackingService.Features.LogWeight
{
    public class UserWeightUpdatedEvent
    {
        public Guid UserId { get; set; }
        public decimal NewWeightKg { get; set; }
    }
}
