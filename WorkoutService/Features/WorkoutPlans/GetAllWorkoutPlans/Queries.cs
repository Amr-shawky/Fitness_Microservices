using MediatR;
using WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans.ViewModels;

namespace WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans
{
    public record GetAllWorkoutPlansQuery : IRequest<PaginatedWorkoutPlansVm>;
}
