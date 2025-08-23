using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestOrderRepository
    {
        Task<ListResponseDTO<TestOrderRowDTO>> GetAllAsync();

        Task<TestOrder> GetByIdAsync(int id);

        Task<int> AddAsync(TestOrder testOrder);

        Task<bool> UpdateAsync(TestOrder testOrder);

        Task<bool> DeleteAsync(int id);
    }
}