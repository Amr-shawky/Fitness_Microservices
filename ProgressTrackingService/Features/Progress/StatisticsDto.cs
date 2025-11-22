namespace ProgressTrackingService.Features.Progress
{
    public record StatisticsDto(int TotalWorkouts, int TotalCaloriesBurned, int CurrentStreak, int LongestStreak, decimal CurrentWeight);

}