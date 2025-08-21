using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface ITestTypeRepository
    {
        Task<IEnumerable<TestType>> GetAllAsync();
        Task<TestType> GetByIdAsync(int id);
        Task<int> AddAsync(TestType testType);
        Task<bool> UpdateAsync(TestType testType);
        Task<bool> DeleteAsync(int id);
    }
}