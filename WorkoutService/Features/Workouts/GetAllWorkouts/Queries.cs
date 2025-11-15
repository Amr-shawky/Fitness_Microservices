using MediatR;
using WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels;

namespace WorkoutService.Features.Workouts.GetAllWorkouts
{
    public record GetAllWorkoutsQuery : IRequest<PaginatedWorkoutsVm>;
}
