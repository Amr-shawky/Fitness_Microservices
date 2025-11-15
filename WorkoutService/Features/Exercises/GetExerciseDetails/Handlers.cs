using MediatR;
using WorkoutService.Features.Exercises.CreateExercise.ViewModels;
using WorkoutService.Domain.Interfaces;

namespace WorkoutService.Features.Exercises.GetExerciseDetails
{
    public class GetExerciseDetailsHandler : IRequestHandler<GetExerciseDetailsQuery, ExerciseVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetExerciseDetailsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExerciseVm> Handle(GetExerciseDetailsQuery request, CancellationToken cancellationToken)
        {
            var exercise = await _unitOfWork.Exercises.GetByIdAsync(request.Id);
            if (exercise == null)
            {
                return null;
            }
            return new ExerciseVm(exercise.Id, exercise.Name, exercise.Description);
        }
    }
}
