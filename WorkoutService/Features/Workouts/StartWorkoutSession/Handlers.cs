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
            // Project only the necessary fields: Id and WorkoutExercises (Id, Order)
            var workoutData = await _workoutRepository.GetAll()
                .Where(w => w.Id == request.WorkoutId)
                .Select(w => new
                {
                    w.Id,
                    Exercises = w.WorkoutExercises.Select(we => new
                    {
                        we.Id,
                        we.Order
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (workoutData == null)
            {
                return RequestResponse<WorkoutSessionViewModel>.Fail("Workout not found");
            }

            var session = new WorkoutSession
            {
                UserId = Guid.NewGuid(), // Mocking user ID for now
                WorkoutId = workoutData.Id,
                Status = "InProgress",
                StartedAt = DateTime.UtcNow,
                PlannedDurationInMinutes = request.Dto.PlannedDuration,
                Difficulty = request.Dto.Difficulty
            };

            var sessionExercises = workoutData.Exercises.Select(e => new WorkoutSessionExercise
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
