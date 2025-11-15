using MediatR;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.WorkoutPlans.AssignPlanToWorkout
{
    public class AssignPlanToWorkoutHandler : IRequestHandler<AssignPlanToWorkoutCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssignPlanToWorkoutHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AssignPlanToWorkoutCommand request, CancellationToken cancellationToken)
        {
            var workoutPlan = await _unitOfWork.WorkoutPlans.GetByIdAsync(request.PlanId);
            var workout = await _unitOfWork.Workouts.GetByIdAsync(request.WorkoutId);

            if (workoutPlan == null || workout == null)
            {
                // Or throw a custom exception
                return Unit.Value;
            }

            workout.WorkoutPlanId = workoutPlan.Id;
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
