using WorkoutService.Features.Shared;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;

namespace WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels
{
    // Change from 'record' to 'class' because records cannot inherit from non-record types.
    public class PaginatedWorkoutsVm : PaginatedResult<WorkoutVm>
    {
        public PaginatedWorkoutsVm(List<WorkoutVm> items, int count, int pageNumber, int pageSize)
            : base(items, count, pageNumber, pageSize)
        {
        }
    }
}
