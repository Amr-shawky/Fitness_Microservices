using Mapster;
using MediatR;
using WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.Workouts.GetAllWorkouts
{
    public class GetAllWorkoutsHandler : IRequestHandler<GetAllWorkoutsQuery, PaginatedWorkoutsVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Workout> _workoutRepository;
        public GetAllWorkoutsHandler(IUnitOfWork unitOfWork , IBaseRepository<Workout> workoutRepository)
        {
            _unitOfWork = unitOfWork;
            _workoutRepository = workoutRepository;
        }

        public async Task<PaginatedWorkoutsVm> Handle(GetAllWorkoutsQuery request, CancellationToken cancellationToken)
        {
            var workouts = await _workoutRepository.GetAllAsync();
            var workoutVms = workouts.Adapt<List<WorkoutVm>>();
            return new PaginatedWorkoutsVm(workoutVms);
        }
    }
}
