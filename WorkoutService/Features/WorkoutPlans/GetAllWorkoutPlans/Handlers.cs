using Mapster;
using MediatR;
using WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans.ViewModels;
using WorkoutService.Domain.Interfaces;

namespace WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans
{
    public class GetAllWorkoutPlansHandler : IRequestHandler<GetAllWorkoutPlansQuery, PaginatedWorkoutPlansVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllWorkoutPlansHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedWorkoutPlansVm> Handle(GetAllWorkoutPlansQuery request, CancellationToken cancellationToken)
        {
            var workoutPlans = await _unitOfWork.WorkoutPlans.GetAllAsync();
            var workoutPlanVms = workoutPlans.Adapt<List<WorkoutPlanVm>>();
            return new PaginatedWorkoutPlansVm(workoutPlanVms);
        }
    }
}
