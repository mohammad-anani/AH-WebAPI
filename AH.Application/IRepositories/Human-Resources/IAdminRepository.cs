using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IAdminRepository : IEmployee
    {
        Task<(IEnumerable<AdminRowDTO> Items, int Count)> GetAllAsync(AdminFilterDTO filterDTO);

        Task<Admin> GetByIdAsync(int id);

        Task<int> AddAsync(Admin admin);

        Task<bool> UpdateAsync(Admin admin);

        Task<bool> DeleteAsync(int id);
    }
}