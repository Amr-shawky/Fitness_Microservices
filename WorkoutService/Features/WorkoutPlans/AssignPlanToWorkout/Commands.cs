using MediatR;

namespace WorkoutService.Features.WorkoutPlans.AssignPlanToWorkout
{
    public record AssignPlanToWorkoutCommand(int PlanId, int WorkoutId) : IRequest;
}
