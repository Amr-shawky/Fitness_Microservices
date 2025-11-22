using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using ProgressTrackingService.Data;
using ProgressTrackingService.Models;
using ProgressTrackingService.Shared;

namespace ProgressTrackingService.Features.LogWeight
{
    public class LogWeightHandler : IRequestHandler<LogWeightCommand, EndpointResponse<WeightDto>>
    {
        private readonly ProgressDbContext _db;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;

        public LogWeightHandler(
            ProgressDbContext db,
            IHttpClientFactory httpClientFactory,
            IMemoryCache cache)
        {
            _db = db;
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<EndpointResponse<WeightDto>> Handle(LogWeightCommand req, CancellationToken ct)
        {
            // Create entry
            var entry = new WeightEntry
            {
                Id = Guid.NewGuid(),
                UserId = req.UserId,
                WeightKg = req.WeightKg,
                LoggedAt = req.LoggedAt
            };

            using var tx = await _db.Database.BeginTransactionAsync(ct);
            _db.WeightEntries.Add(entry);

            // Update statistics
            var stats = await _db.UserStatistics.SingleOrDefaultAsync(s => s.UserId == req.UserId, ct);
            if (stats == null)
            {
                stats = new UserStatistics
                {
                    Id = Guid.NewGuid(),
                    UserId = req.UserId,
                    StartingWeight = req.WeightKg,
                    CurrentWeight = req.WeightKg,
                    CreatedAt = DateTimeOffset.UtcNow
                };
                _db.UserStatistics.Add(stats);
            }
            else
            {
                if (stats.StartingWeight == 0)
                    stats.StartingWeight = req.WeightKg;

                stats.CurrentWeight = req.WeightKg;
                stats.UpdatedAt = DateTimeOffset.UtcNow;
            }

            await _db.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);

            // ------------ NEW: CALL WORKOUT SERVICE INSTEAD OF PUBLISH EVENT ------------
            var client = _httpClientFactory.CreateClient("workout-service");

            var payload = new
            {
                userId = req.UserId,
                newWeight = req.WeightKg
            };

            await client.PostAsJsonAsync("/api/v1/workout/recalculate", payload, ct);

            // ------------ MEMORY CACHE INVALIDATION ------------
            _cache.Remove($"progress_dashboard_{req.UserId}");

            var dto = new WeightDto
            {
                Id = entry.Id,
                WeightKg = entry.WeightKg,
                LoggedAt = entry.LoggedAt
            };

            return EndpointResponse<WeightDto>.SuccessResponse(dto, "Weight logged successfully");
        }
    }
}