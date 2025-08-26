using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter.Finance;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface IInsuranceRepository
    {
        Task<GetAllResponseDTO<InsuranceRowDTO>> GetAllByPatientIDAsync(InsuranceFilterDTO filterDTO);

        Task<GetByIDResponseDTO<InsuranceDTO>> GetByIDAsync(int id);

        Task<bool> Renew(int id);

        Task<int> AddAsync(Insurance insurance);

        Task<bool> UpdateAsync(Insurance insurance);

        Task<bool> DeleteAsync(int id);
    }
}