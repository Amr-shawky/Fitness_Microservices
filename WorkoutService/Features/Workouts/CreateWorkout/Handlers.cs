using MediatR;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.Workouts.CreateWorkout
{
    public class CreateWorkoutHandler : IRequestHandler<CreateWorkoutCommand, WorkoutVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateWorkoutHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutVm> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
        {
            // TODO: Add mapping
            var workout = new Workout { Name = request.Dto.Name, Description = request.Dto.Description };
            await _unitOfWork.Workouts.AddAsync(workout);
            await _unitOfWork.CompleteAsync();
            return new WorkoutVm(workout.Id, workout.Name, workout.Description);
        }
    }
}
