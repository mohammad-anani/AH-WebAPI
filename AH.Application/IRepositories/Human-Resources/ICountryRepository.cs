using AH.Application.DTOs.Response;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface ICountryRepository
    {
        Task<GetAllResponseDTO<Country>> GetAllAsync();
    }
}
