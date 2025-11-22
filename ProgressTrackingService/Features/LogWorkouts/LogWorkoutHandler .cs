using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProgressTrackingService.Data;
using ProgressTrackingService.Models;
using ProgressTrackingService.Shared;

namespace ProgressTrackingService.Features.LogWorkout
{
    public class LogWorkoutHandler : IRequestHandler<LogWorkoutCommand, EndpointResponse<WorkoutLogDto>>
    {
        private readonly ProgressDbContext _db;
        private readonly IMemoryCache _memoryCache;
        // Optionally a distributed cache / event bus
        public LogWorkoutHandler(ProgressDbContext db, IMemoryCache memoryCache)
        {
            _db = db;
            _memoryCache = memoryCache;
        }

        public async Task<EndpointResponse<WorkoutLogDto>> Handle(LogWorkoutCommand req, CancellationToken ct)
        {
            // Idempotency check (simple): ensure no existing log with same client request id
            // For production prefer a dedicated Idempotency table or request-ids on resource.
            var exists = await _db.WorkoutLogs.AnyAsync(w => w.SessionId == req.SessionId && w.UserId == req.UserId, ct);
            if (exists)
            {
                return EndpointResponse<WorkoutLogDto>.SuccessResponse(null, "Already logged"); // or return existing dto
            }

            var log = new WorkoutLog
            {
                Id = Guid.NewGuid(),
                UserId = req.UserId,
                SessionId = req.SessionId,
                WorkoutId = req.WorkoutId,
                DurationMinutes = req.DurationMinutes,
                CaloriesBurned = req.CaloriesBurned,
                Rating = req.Rating,
                PerformedAt = req.PerformedAt
            };

            using var tx = await _db.Database.BeginTransactionAsync(ct);
            _db.WorkoutLogs.Add(log);

            // Update or create user statistics
            var stats = await _db.UserStatistics.SingleOrDefaultAsync(s => s.UserId == req.UserId, ct);
            if (stats == null)
            {
                stats = new UserStatistics
                {
                    Id = Guid.NewGuid(),
                    UserId = req.UserId,
                    TotalWorkouts = 1,
                    TotalCaloriesBurned = req.CaloriesBurned,
                    CurrentStreak = 1,
                    LongestStreak = 1,
                    LastWorkoutAt = req.PerformedAt,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                };
                _db.UserStatistics.Add(stats);
            }
            else
            {
                stats.TotalWorkouts += 1;
                stats.TotalCaloriesBurned += req.CaloriesBurned;
                // compute streak
                var yesterday = req.PerformedAt.Date.AddDays(-1);
                if (stats.LastWorkoutAt.Date == yesterday)
                    stats.CurrentStreak += 1;
                else if (stats.LastWorkoutAt.Date == req.PerformedAt.Date)
                    ; // same day duplicate
                else
                    stats.CurrentStreak = 1;

                if (stats.CurrentStreak > stats.LongestStreak) stats.LongestStreak = stats.CurrentStreak;
                stats.LastWorkoutAt = req.PerformedAt;
                stats.UpdatedAt = DateTimeOffset.UtcNow;
                _db.UserStatistics.Update(stats);
            }

            await _db.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);

            // Cache invalidation for user's dashboard
            var cacheKey = $"progress_dashboard_{req.UserId}";
            _memoryCache.Remove(cacheKey); // if using distributed cache, remove key there

            // Achievements: you can fire internal domain events or call achievements service
            // return dto
            var dto = new WorkoutLogDto
            {
                Id = log.Id,
                CaloriesBurned = log.CaloriesBurned,
                DurationMinutes = log.DurationMinutes,
                PerformedAt = log.PerformedAt
            };
            return EndpointResponse<WorkoutLogDto>.SuccessResponse(dto, "Workout logged");
        }
    }
}
