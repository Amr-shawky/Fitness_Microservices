using MediatR;
using WorkoutService.Features.Exercises.CreateExercise.ViewModels;

namespace WorkoutService.Features.Exercises.CreateExercise
{
    public record CreateExerciseCommand(CreateExerciseDto Dto) : IRequest<ExerciseVm>;
}
