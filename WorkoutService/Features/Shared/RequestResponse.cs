namespace WorkoutService.Features.Shared
{
    public record RequestResponse<T>(
            T? Data,
            string Message = "",
            string MessageAr = "",
            bool IsSuccess = true
        )
    {
        public static RequestResponse<T> Success(
            T data,
            string message = "Operation completed successfully",
            string messageAr = "العملية تمت بنجاح"
        ) => new(data, message, messageAr, true);

        public static RequestResponse<T> Fail(
            string message = "Operation failed",
            string messageAr = "فشل العملية"
        ) => new(default!, message, messageAr, false);
    }
}
