using MediatR;
using ProgressTrackingService.Shared;

namespace ProgressTrackingService.Features.LogWeight
{
    public record LogWeightCommand(Guid UserId, decimal WeightKg, DateTimeOffset LoggedAt, string ClientRequestId) : IRequest<EndpointResponse<WeightDto>>;

}
