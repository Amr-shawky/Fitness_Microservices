namespace WorkoutService.Features.WorkoutPlans.GetAllWorkoutPlans.ViewModels
{
    public record WorkoutPlanVm(int Id, string Name, string Description);
    public record PaginatedWorkoutPlansVm(List<WorkoutPlanVm> WorkoutPlans);
}
