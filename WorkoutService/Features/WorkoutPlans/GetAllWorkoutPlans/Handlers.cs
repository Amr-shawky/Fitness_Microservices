using Mapster;
using MediatR;
using WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans
{
    public class GetAllWorkoutPlansHandler : IRequestHandler<GetAllWorkoutPlansQuery, PaginatedWorkoutPlansVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<WorkoutPlan> _workoutPlanRepository;
        public GetAllWorkoutPlansHandler(IUnitOfWork unitOfWork , IBaseRepository<WorkoutPlan> workoutPlanRepository)
        {
            _unitOfWork = unitOfWork;
            _workoutPlanRepository = workoutPlanRepository;
        }

        public async Task<PaginatedWorkoutPlansVm> Handle(GetAllWorkoutPlansQuery request, CancellationToken cancellationToken)
        {
            var workoutPlans = await _workoutPlanRepository.GetAllAsync();
            var workoutPlanVms = workoutPlans.Adapt<List<WorkoutPlanVm>>();
            return new PaginatedWorkoutPlansVm(workoutPlanVms);
        }
    }
}
