using Mapster;
using MediatR;
using WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;

namespace WorkoutService.Features.Workouts.GetWorkoutsByCategory
{
    public class GetWorkoutsByCategoryHandler : IRequestHandler<GetWorkoutsByCategoryQuery, PaginatedWorkoutsVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetWorkoutsByCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedWorkoutsVm> Handle(GetWorkoutsByCategoryQuery request, CancellationToken cancellationToken)
        {
            // This is a simplistic implementation. A real-world scenario would likely involve
            // a more complex filtering mechanism, possibly at the repository or database level.
            var allWorkouts = await _unitOfWork.Workouts.GetAllAsync();
            var workoutsByCategory = allWorkouts.Where(w => w.Category.Equals(request.Category, StringComparison.OrdinalIgnoreCase));

            var workoutVms = workoutsByCategory.Adapt<List<WorkoutVm>>();
            return new PaginatedWorkoutsVm(workoutVms);
        }
    }
}
