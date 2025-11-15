using MediatR;
using WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.WorkoutPlans.GetWorkoutPlanDetails
{
    public class GetWorkoutPlanDetailsHandler : IRequestHandler<GetWorkoutPlanDetailsQuery, WorkoutPlanVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<WorkoutPlan> _workoutPlanRepository;
        public GetWorkoutPlanDetailsHandler(IUnitOfWork unitOfWork , IBaseRepository<WorkoutPlan> workoutPlanRepository)
        {
            _unitOfWork = unitOfWork;
            _workoutPlanRepository = workoutPlanRepository;
        }

        public async Task<WorkoutPlanVm> Handle(GetWorkoutPlanDetailsQuery request, CancellationToken cancellationToken)
        {
            var workoutPlan = await _workoutPlanRepository.GetByIdAsync(request.Id);
            if (workoutPlan == null)
            {
                return null;
            }
            return new WorkoutPlanVm(workoutPlan.Id, workoutPlan.Name, workoutPlan.Description);
        }
    }
}
