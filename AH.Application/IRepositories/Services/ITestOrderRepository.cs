using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestOrderRepository
    {
        Task<GetAllResponseDTO<TestOrderRowDTO>> GetAllAsync(TestOrderFilterDTO filterDTO);

        Task<GetByIDResponseDTO<TestOrderDTO>> GetByIDAsync(int id);

        Task<CreateResponseDTO> AddAsync(TestOrder testOrder);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}