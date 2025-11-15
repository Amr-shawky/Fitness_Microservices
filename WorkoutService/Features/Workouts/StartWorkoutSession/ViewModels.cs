namespace WorkoutService.Features.Workouts.StartWorkoutSession.ViewModels
{
    public record WorkoutSessionVm(int Id, int WorkoutId, DateTime StartTime, DateTime? EndTime);
}
