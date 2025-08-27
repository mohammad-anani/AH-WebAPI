using AH.Application.DTOs.Response;

namespace AH.Application.IRepositories
{
    public interface IService
    {
        Task<SuccessResponseDTO> StartAsync(int id, string? notes);

        Task<SuccessResponseDTO> CancelAsync(int id, string? notes);

        Task<SuccessResponseDTO> CompleteAsync(int id, string? notes, string result);

        Task<SuccessResponseDTO> RescheduleAsync(int id, string? notes, DateTime newScheduledDate);
    }
}