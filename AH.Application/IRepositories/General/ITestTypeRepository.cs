using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestTypeRepository
    {
        Task<ListResponseDTO<TestTypeRowDTO>> GetAllAsync(TestTypeFilterDTO filterDTO);

        Task<TestType> GetByIdAsync(int id);

        Task<int> AddAsync(TestType testType);

        Task<bool> UpdateAsync(TestType testType);

        Task<bool> DeleteAsync(int id);
    }
}