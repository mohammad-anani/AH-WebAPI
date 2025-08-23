using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

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

        Task<(IEnumerable<TestOrderRowDTO> Items, int Count)> ITestOrderRepository.GetAllAsync()
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