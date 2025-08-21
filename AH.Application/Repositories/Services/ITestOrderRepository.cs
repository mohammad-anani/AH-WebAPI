using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface ITestOrderRepository
    {
        Task<IEnumerable<TestOrder>> GetAllAsync();
        Task<TestOrder> GetByIdAsync(int id);
        Task<int> AddAsync(TestOrder testOrder);
        Task<bool> UpdateAsync(TestOrder testOrder);
        Task<bool> DeleteAsync(int id);
    }
}