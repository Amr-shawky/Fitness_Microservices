using WorkoutService.Features.Exercises.CreateExercise.ViewModels;

namespace WorkoutService.Features.Exercises.GetAllExercises.ViewModels
{
    public record PaginatedExercisesVm(List<ExerciseVm> Exercises);
}
