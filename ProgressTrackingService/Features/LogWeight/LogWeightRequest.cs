namespace ProgressTrackingService.Features.LogWeight
{
    public record LogWeightRequest(Guid UserId, decimal WeightKg, DateTimeOffset LoggedAt, string ClientRequestId);

}
