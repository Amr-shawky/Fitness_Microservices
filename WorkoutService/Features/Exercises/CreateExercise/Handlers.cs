using MediatR;
using WorkoutService.Features.Exercises.CreateExercise.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.Exercises.CreateExercise
{
    public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, ExerciseVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Exercise> _exerciseRepository;
        public CreateExerciseHandler(IUnitOfWork unitOfWork, IBaseRepository<Exercise> exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ExerciseVm> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            // TODO: Add mapping
            var exercise = new Exercise { Name = request.Dto.Name, Description = request.Dto.Description };
            await _exerciseRepository.AddAsync(exercise);
            await _unitOfWork.CompleteAsync();
            return new ExerciseVm(exercise.Id, exercise.Name, exercise.Description);
        }
    }
}
