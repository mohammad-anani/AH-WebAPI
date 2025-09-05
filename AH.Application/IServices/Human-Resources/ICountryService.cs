using AH.Application.DTOs.Response;
using AH.Domain.Entities;

namespace AH.Application.IServices
{
    public interface ICountryService
    {
        Task<ServiceResult<GetAllResponseDataDTO<Country>>> GetAllAsync();
    }
}
