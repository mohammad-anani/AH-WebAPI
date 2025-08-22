using AH.Domain.Entities;
using AH.Application.Repositories;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AH.Application.DTOs.Row;
using System.Threading.Tasks;

namespace AH.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public async Task<Tuple<IEnumerable<DepartmentRowDTO>, int>> GetAllAsync(DepartmentFilterDTO filterDTO)
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