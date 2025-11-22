using ProgressTrackingService.Features.LogWeight;
using ProgressTrackingService.Features.LogWorkout;

namespace ProgressTrackingService.Features.Progress
{
    public record ProgressDashboardDto(StatisticsDto? Statistics, List<WeightDto> WeightHistory, List<WorkoutLogDto> RecentWorkouts, List<object> Achievements);

}