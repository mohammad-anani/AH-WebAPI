using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestTypeRepository
    {
        Task<GetAllResponseDTO<TestTypeRowDTO>> GetAllAsync(TestTypeFilterDTO filterDTO);

        Task<GetByIDResponseDTO<TestTypeDTO>> GetByIDAsync(int id);

        Task<CreateResponseDTO> AddAsync(TestType testType);

        Task<SuccessResponseDTO> UpdateAsync(TestType testType);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}