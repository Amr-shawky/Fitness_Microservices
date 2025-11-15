using Mapster;
using MediatR;
using WorkoutService.Features.Exercises.GetAllExercises.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features.Exercises.CreateExercise.ViewModels;

namespace WorkoutService.Features.Exercises.GetAllExercises
{
    public class GetAllExercisesHandler : IRequestHandler<GetAllExercisesQuery, PaginatedExercisesVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllExercisesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedExercisesVm> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _unitOfWork.Exercises.GetAllAsync();
            var exerciseVms = exercises.Adapt<List<ExerciseVm>>();
            return new PaginatedExercisesVm(exerciseVms);
        }
    }
}
