using AH.Domain.Entities;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Tuple<IEnumerable<Department>, int>> GetAllAsync(DepartmentFilterDTO filterDTO);
        Task<Department> GetByIdAsync(int id);
        Task<int> AddAsync(Department department);
        Task<bool> UpdateAsync(Department department);
        Task<bool> DeleteAsync(int id);
    }
}