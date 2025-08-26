using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestTypeRepository
    {
        Task<GetAllResponseDTO<TestTypeRowDTO>> GetAllAsync(TestTypeFilterDTO filterDTO);

        Task<GetByIDResponseDTO<TestTypeDTO>> GetByIDAsync(int id);

        Task<int> AddAsync(TestType testType);

        Task<bool> UpdateAsync(TestType testType);

        Task<bool> DeleteAsync(int id);
    }
}