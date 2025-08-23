using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestOrderRepository
    {
        Task<(IEnumerable<TestOrderRowDTO> Items, int Count)> GetAllAsync();

        Task<TestOrder> GetByIdAsync(int id);

        Task<int> AddAsync(TestOrder testOrder);

        Task<bool> UpdateAsync(TestOrder testOrder);

        Task<bool> DeleteAsync(int id);
    }
}