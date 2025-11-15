using MediatR;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;
using WorkoutService.Domain.Interfaces;

namespace WorkoutService.Features.Workouts.GetWorkoutDetails
{
    public class GetWorkoutDetailsHandler : IRequestHandler<GetWorkoutDetailsQuery, WorkoutVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetWorkoutDetailsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutVm> Handle(GetWorkoutDetailsQuery request, CancellationToken cancellationToken)
        {
            var workout = await _unitOfWork.Workouts.GetByIdAsync(request.Id);
            if (workout == null)
            {
                return null;
            }
            return new WorkoutVm(workout.Id, workout.Name, workout.Description);
        }
    }
}
