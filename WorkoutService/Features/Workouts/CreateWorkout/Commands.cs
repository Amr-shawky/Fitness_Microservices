using MediatR;
using WorkoutService.Features.Workouts.CreateWorkout.ViewModels;

namespace WorkoutService.Features.Workouts.CreateWorkout
{
    public record CreateWorkoutCommand(CreateWorkoutDto Dto) : IRequest<WorkoutVm>;
}
