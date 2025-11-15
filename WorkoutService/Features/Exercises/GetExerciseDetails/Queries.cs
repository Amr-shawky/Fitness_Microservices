using MediatR;
using WorkoutService.Features.Exercises.CreateExercise.ViewModels;

namespace WorkoutService.Features.Exercises.GetExerciseDetails
{
    public record GetExerciseDetailsQuery(int Id) : IRequest<ExerciseVm>;
}
