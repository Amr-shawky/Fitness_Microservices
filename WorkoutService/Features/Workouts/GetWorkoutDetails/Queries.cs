using MediatR;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;

namespace WorkoutService.Features.Workouts.GetWorkoutDetails
{
    public record GetWorkoutDetailsQuery(int Id) : IRequest<WorkoutVm>;
}
