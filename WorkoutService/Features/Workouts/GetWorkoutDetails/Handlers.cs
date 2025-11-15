using MediatR;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.Workouts.GetWorkoutDetails
{
    public class GetWorkoutDetailsHandler : IRequestHandler<GetWorkoutDetailsQuery, WorkoutVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Workout> _workoutRepository;
        public GetWorkoutDetailsHandler(IUnitOfWork unitOfWork , IBaseRepository<Workout> workoutRepository)
        {
            _unitOfWork = unitOfWork;
            _workoutRepository = workoutRepository;
        }

        public async Task<WorkoutVm> Handle(GetWorkoutDetailsQuery request, CancellationToken cancellationToken)
        {
            var workout = await _workoutRepository.GetByIdAsync(request.Id);
            if (workout == null)
            {
                return null;
            }
            return new WorkoutVm(workout.Id, workout.Name, workout.Description);
        }
    }
}
