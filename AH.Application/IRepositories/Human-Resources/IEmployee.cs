using AH.Application.DTOs.Response;

namespace AH.Application.IRepositories
{
    public interface IEmployee
    {
        Task<SuccessResponseDTO> LeaveAsync(int ID);
    }
}