using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IServices
{
    public interface ITestOrderService
    {
        Task<GetAllResponseDataDTO<TestOrderRowDTO>> GetAllAsync(TestOrderFilterDTO filterDTO);

        Task<TestOrderDTO> GetByIDAsync(int id);

        Task<int> AddAsync(TestOrder testOrder);

        Task<bool> DeleteAsync(int id);
    }
}