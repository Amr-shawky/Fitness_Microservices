using MediatR;
using WorkoutService.Features.Workouts.StartWorkoutSession.ViewModels;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Domain.Entities;

namespace WorkoutService.Features.Workouts.StartWorkoutSession
{
    public class StartWorkoutSessionHandler : IRequestHandler<StartWorkoutSessionCommand, WorkoutSessionVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        public StartWorkoutSessionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutSessionVm> Handle(StartWorkoutSessionCommand request, CancellationToken cancellationToken)
        {
            var session = new WorkoutSession
            {
                WorkoutId = request.Dto.WorkoutId,
                StartTime = request.Dto.StartTime,
                EndTime = null // Session has not ended yet
            };

            await _unitOfWork.WorkoutSessions.AddAsync(session);
            await _unitOfWork.CompleteAsync();

            return new WorkoutSessionVm(session.Id, session.WorkoutId, session.StartTime, session.EndTime);
        }
    }
}
