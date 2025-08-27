using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface IAdminRepository : IEmployee
    {
        Task<GetAllResponseDTO<AdminRowDTO>> GetAllAsync(AdminFilterDTO filterDTO);

        Task<GetByIDResponseDTO<AdminDTO>> GetByIDAsync(int id);

        Task<int> AddAsync(Admin admin);

        Task<bool> UpdateAsync(Admin admin);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}