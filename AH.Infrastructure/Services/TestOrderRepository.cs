using AH.Domain.Entities;
using AH.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Application.DTOs.Row;

namespace AH.Infrastructure.Repositories
{
    public class TestOrderRepository : ITestOrderRepository
    {
       public async Task<int> AddAsync(TestOrder testOrder)
        {
            throw new NotImplementedException();
        }

        Task<bool> ITestOrderRepository.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Tuple<IEnumerable<TestOrderRowDTO>, int>> ITestOrderRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<TestOrder> ITestOrderRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> ITestOrderRepository.UpdateAsync(TestOrder testOrder)
        {
            throw new NotImplementedException();
        }
    }
}