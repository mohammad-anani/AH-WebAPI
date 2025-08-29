namespace AH.Application.DTOs.Response
{
    public class DeleteResponseDTO
    {
        public bool Success { get; set; }
        public Exception? Exception { get; set; }

        public DeleteResponseDTO(bool success, Exception? exception)
        {
            Success = success;
            Exception = exception;
        }
    }
}