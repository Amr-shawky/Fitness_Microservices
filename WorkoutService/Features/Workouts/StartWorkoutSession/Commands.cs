using MediatR;
using WorkoutService.Features.Workouts.StartWorkoutSession.ViewModels;

namespace WorkoutService.Features.Workouts.StartWorkoutSession
{
    public record StartWorkoutSessionCommand(StartWorkoutSessionDto Dto) : IRequest<WorkoutSessionVm>;
}
