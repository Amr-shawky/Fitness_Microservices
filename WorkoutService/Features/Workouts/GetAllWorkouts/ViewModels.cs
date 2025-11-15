using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;

namespace WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels
{
    public record PaginatedWorkoutsVm(List<WorkoutVm> Workouts);
}
