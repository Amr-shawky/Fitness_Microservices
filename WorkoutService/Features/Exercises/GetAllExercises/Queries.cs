using MediatR;
using WorkoutService.Features.Exercises.GetAllExercises.ViewModels;

namespace WorkoutService.Features.Exercises.GetAllExercises
{
    public record GetAllExercisesQuery : IRequest<PaginatedExercisesVm>;
}
