using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IDepartmentRepository
    {
        Task<ListResponseDTO<DepartmentRowDTO>> GetAllAsync(DepartmentFilterDTO filterDTO);

        Task<Department> GetByIdAsync(int id);

        Task<int> AddAsync(Department department);

        Task<bool> UpdateAsync(Department department);

        Task<bool> DeleteAsync(int id);
    }
}