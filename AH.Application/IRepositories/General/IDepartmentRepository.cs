using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IDepartmentRepository
    {
        Task<GetAllResponseDTO<DepartmentRowDTO>> GetAllAsync(DepartmentFilterDTO filterDTO);

        Task<GetByIDResponseDTO<Department>> GetByIDAsync(int id);

        Task<int> AddAsync(Department department);

        Task<bool> UpdateAsync(Department department);

        Task<bool> DeleteAsync(int id);
    }
}