using AH.Application.DTOs.Response;

namespace AH.Application.IServices
{
    public interface IEmployee
    {
        public Task<ServiceResult<bool>> LeaveAsync(int id);
    }
}