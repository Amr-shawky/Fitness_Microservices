using MediatR;
using WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans.ViewModels;

namespace WorkoutService.Features.WorkoutPlans.GetWorkoutPlanDetails
{
    public record GetWorkoutPlanDetailsQuery(int Id) : IRequest<WorkoutPlanVm>;
}
