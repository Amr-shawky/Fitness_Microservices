namespace ProgressTrackingService.Shared
{
    // Shared/EndpointResponse.cs
    public class EndpointResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }

        public static EndpointResponse<T> SuccessResponse(T data, string message = "") =>
            new EndpointResponse<T> { Success = true, Data = data, Message = message };

        public static EndpointResponse<T> NotFoundResponse(string message = "") =>
            new EndpointResponse<T> { Success = false, Message = message };
    }

  

}
