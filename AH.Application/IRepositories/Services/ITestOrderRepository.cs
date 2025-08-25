using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestOrderRepository
    {
        Task<GetAllResponseDTO<TestOrderRowDTO>> GetAllAsync(TestOrderFilterDTO filterDTO);

        Task<GetByIDResponseDTO<TestOrder>> GetByIDAsync(int id);

        Task<int> AddAsync(TestOrder testOrder);

        Task<bool> UpdateAsync(TestOrder testOrder);

        Task<bool> DeleteAsync(int id);
    }
}