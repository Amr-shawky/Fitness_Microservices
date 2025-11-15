using MediatR;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.Workouts.CreateWorkout
{
    public class CreateWorkoutHandler : IRequestHandler<CreateWorkoutCommand, WorkoutVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Workout> _workoutRepository;
        public CreateWorkoutHandler(IUnitOfWork unitOfWork , IBaseRepository<Workout> workoutRepository)
        {
            _unitOfWork = unitOfWork;
            _workoutRepository = workoutRepository;
        }

        public async Task<WorkoutVm> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
        {
            // TODO: Add mapping
            var workout = new Workout { Name = request.Dto.Name, Description = request.Dto.Description };
            await _workoutRepository.AddAsync(workout);
            await _unitOfWork.CompleteAsync();
            return new WorkoutVm(workout.Id, workout.Name, workout.Description);
        }
    }
}
