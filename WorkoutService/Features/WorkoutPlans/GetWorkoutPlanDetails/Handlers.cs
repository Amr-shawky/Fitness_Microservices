using MediatR;
using WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans.ViewModels;
using WorkoutService.Domain.Interfaces;

namespace WorkoutService.Features.WorkoutPlans.GetWorkoutPlanDetails
{
    public class GetWorkoutPlanDetailsHandler : IRequestHandler<GetWorkoutPlanDetailsQuery, WorkoutPlanVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetWorkoutPlanDetailsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutPlanVm> Handle(GetWorkoutPlanDetailsQuery request, CancellationToken cancellationToken)
        {
            var workoutPlan = await _unitOfWork.WorkoutPlans.GetByIdAsync(request.Id);
            if (workoutPlan == null)
            {
                return null;
            }
            return new WorkoutPlanVm(workoutPlan.Id, workoutPlan.Name, workoutPlan.Description);
        }
    }
}
