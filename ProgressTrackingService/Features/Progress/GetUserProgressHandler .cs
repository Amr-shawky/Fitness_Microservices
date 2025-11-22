using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProgressTrackingService.Data;
using ProgressTrackingService.Features.LogWeight;
using ProgressTrackingService.Features.LogWorkout;
using ProgressTrackingService.Shared;

namespace ProgressTrackingService.Features.Progress
{
    public class GetUserProgressHandler
        : IRequestHandler<GetUserProgressQuery, EndpointResponse<ProgressDashboardDto>>
    {
        private readonly ProgressDbContext _db;
        private readonly IMemoryCache _cache;

        public GetUserProgressHandler(ProgressDbContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public async Task<EndpointResponse<ProgressDashboardDto>> Handle(
            GetUserProgressQuery req, CancellationToken ct)
        {
            var cacheKey = $"progress_dashboard_{req.UserId}_{req.Period}";

            // Try get cached value
            if (_cache.TryGetValue(cacheKey, out ProgressDashboardDto cachedDto))
            {
                return EndpointResponse<ProgressDashboardDto>
                    .SuccessResponse(cachedDto, "From memory cache");
            }

            // ✔ Query DB
            var stats = await _db.UserStatistics
                .SingleOrDefaultAsync(s => s.UserId == req.UserId, ct);

            var weights = await _db.WeightEntries
                .Where(w => w.UserId == req.UserId &&
                            w.LoggedAt >= DateTimeOffset.UtcNow.AddMonths(-1))
                .OrderBy(w => w.LoggedAt)
                .ToListAsync(ct);

            var recent = await _db.WorkoutLogs
                .Where(w => w.UserId == req.UserId)
                .OrderByDescending(w => w.PerformedAt)
                .Take(20)
                .ToListAsync(ct);

            // ✔ Build DTO
            var dto = new ProgressDashboardDto(
                stats is null
                    ? null
                    : new StatisticsDto(
                        stats.TotalWorkouts,
                        stats.TotalCaloriesBurned,
                        stats.CurrentStreak,
                        stats.LongestStreak,
                        stats.CurrentWeight
                    ),

                weights.Select(w => new WeightDto
                {
                    Id = w.Id,
                    WeightKg = w.WeightKg,
                    LoggedAt = w.LoggedAt
                }).ToList(),

                recent.Select(r => new WorkoutLogDto
                {
                    Id = r.Id,
                    DurationMinutes = r.DurationMinutes,
                    CaloriesBurned = r.CaloriesBurned,
                    PerformedAt = r.PerformedAt
                }).ToList(),

                new List<object>() // achievements stub
            );

            // ✔ Cache to 2 minutes
            _cache.Set(cacheKey, dto, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            });

            return EndpointResponse<ProgressDashboardDto>.SuccessResponse(dto, "OK");
        }
    }
}
