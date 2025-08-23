using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public async Task<ListResponseDTO<DepartmentRowDTO>> GetAllAsync(DepartmentFilterDTO filterDTO)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Department department)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}