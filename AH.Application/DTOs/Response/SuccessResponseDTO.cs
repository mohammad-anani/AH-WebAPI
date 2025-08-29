namespace AH.Application.DTOs.Response
{
    public class SuccessResponseDTO
    {
        public bool Success { get; set; }
        public Exception? Exception { get; set; }

        public SuccessResponseDTO(bool success, Exception? exception)
        {
            Success = success;
            Exception = exception;
        }
    }
}