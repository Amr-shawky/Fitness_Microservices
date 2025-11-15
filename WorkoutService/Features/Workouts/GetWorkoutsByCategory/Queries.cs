using MediatR;
using WorkoutService.Features.Workouts.GetAllWorkouts.ViewModels;

namespace WorkoutService.Features.Workouts.GetWorkoutsByCategory
{
    public record GetWorkoutsByCategoryQuery(string Category) : IRequest<PaginatedWorkoutsVm>;
}
