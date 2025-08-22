using AH.Domain.Entities;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface ITestTypeRepository
    {
        Task<Tuple<IEnumerable<TestType>, int>> GetAllAsync(TestTypeFilterDTO filterDTO);
        Task<TestType> GetByIdAsync(int id);
        Task<int> AddAsync(TestType testType);
        Task<bool> UpdateAsync(TestType testType);
        Task<bool> DeleteAsync(int id);
    }
}