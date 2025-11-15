using MediatR;
using WorkoutService.Domain.Entities;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features.Exercises.CreateExercise.ViewModels;

namespace WorkoutService.Features.Exercises.GetExerciseDetails
{
    public class GetExerciseDetailsHandler : IRequestHandler<GetExerciseDetailsQuery, ExerciseVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Exercise> _exerciseRepository;

        public GetExerciseDetailsHandler(IUnitOfWork unitOfWork , IBaseRepository<Exercise> exerciseRepository)
        {
            _unitOfWork = unitOfWork;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<ExerciseVm> Handle(GetExerciseDetailsQuery request, CancellationToken cancellationToken)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(request.Id);
            if (exercise == null)
            {
                return null;
            }
            return new ExerciseVm(exercise.Id, exercise.Name, exercise.Description);
        }
    }
}
