using MediatR;
using ProgressTrackingService.Shared;

namespace ProgressTrackingService.Features.Progress
{
    public record GetUserProgressQuery(Guid UserId, string Period = "month") : IRequest<EndpointResponse<ProgressDashboardDto>>;

}
