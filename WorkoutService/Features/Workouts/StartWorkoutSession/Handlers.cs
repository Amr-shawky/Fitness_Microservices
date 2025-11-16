using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Domain.Entities;
using WorkoutService.Domain.Interfaces;
using WorkoutService.Features.Shared;
using WorkoutService.Features.Workouts.StartWorkoutSession.ViewModels;
using WorkoutService.Infrastructure.Data;

namespace WorkoutService.Features.Workouts.StartWorkoutSession
{
    public class StartWorkoutSessionCommandHandler : IRequestHandler<StartWorkoutSessionCommand, RequestResponse<WorkoutSessionViewModel>>
    {
        private readonly IBaseRepository<Workout> _workoutRepository;
        private readonly IBaseRepository<WorkoutSession> _workoutSessionRepository;
        private readonly ApplicationDbContext _context;

        public StartWorkoutSessionCommandHandler(IBaseRepository<Workout> workoutRepository, IBaseRepository<WorkoutSession> workoutSessionRepository, ApplicationDbContext context)
        {
            _workoutRepository = workoutRepository;
            _workoutSessionRepository = workoutSessionRepository;
            _context = context;
        }

        public async Task<RequestResponse<WorkoutSessionViewModel>> Handle(StartWorkoutSessionCommand request, CancellationToken cancellationToken)
        {
            var workout = await _workoutRepository.GetAll()
                .Include(w => w.WorkoutExercises)
                .FirstOrDefaultAsync(w => w.Id == request.WorkoutId, cancellationToken);

            if (workout == null)
            {
                return RequestResponse<WorkoutSessionViewModel>.Fail("Workout not found");
            }

            var session = new WorkoutSession
            {
                UserId = Guid.NewGuid(), // Mocking user ID for now
                WorkoutId = workout.Id,
                Status = "InProgress",
                StartedAt = DateTime.UtcNow,
                PlannedDurationInMinutes = request.Dto.PlannedDuration,
                Difficulty = request.Dto.Difficulty
            };

            var sessionExercises = workout.WorkoutExercises.Select(e => new WorkoutSessionExercise
            {
                ExerciseId = e.Id,
                Status = "Pending",
                Order = e.Order
            }).ToList();

            session.SessionExercises = sessionExercises;

            await _workoutSessionRepository.AddAsync(session);
            await _context.SaveChangesAsync(cancellationToken);

            var sessionVm = session.Adapt<WorkoutSessionViewModel>();

            return RequestResponse<WorkoutSessionViewModel>.Success(sessionVm, "Workout session started");
        }
    }
}
