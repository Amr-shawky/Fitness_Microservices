using MediatR;
using WorkoutService.Features.Shared;
using WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels;

namespace WorkoutService.Features.Workouts.GetAllWorkouts
{
    public record GetAllWorkoutsQuery(
        int Page = 1,
        int PageSize = 20,
        string? Category = null,
        string? Difficulty = null,
        int? Duration = null,
        string? Search = null
    ) : IRequest<RequestResponse<PaginatedWorkoutsVm>>;
}
