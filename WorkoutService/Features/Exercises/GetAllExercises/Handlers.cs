using Mapster;
using MediatR;
using WorkoutService.Features.Exercises.GetAllExercises.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features.Exercises.CreateExercise.ViewModels;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.Exercises.GetAllExercises
{
    public class GetAllExercisesHandler : IRequestHandler<GetAllExercisesQuery, PaginatedExercisesVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Exercise> _exerciseRepository;
        public GetAllExercisesHandler(IUnitOfWork unitOfWork , IBaseRepository<Exercise> exerciseRepository)
        {
            _unitOfWork = unitOfWork;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<PaginatedExercisesVm> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _exerciseRepository.GetAllAsync();
            var exerciseVms = exercises.Adapt<List<ExerciseVm>>();
            return new PaginatedExercisesVm(exerciseVms);
        }
    }
}
