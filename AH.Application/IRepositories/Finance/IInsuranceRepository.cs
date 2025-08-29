using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IInsuranceRepository
    {
        Task<GetAllResponseDTO<InsuranceRowDTO>> GetAllByPatientIDAsync(InsuranceFilterDTO filterDTO);

        Task<GetByIDResponseDTO<InsuranceDTO>> GetByIDAsync(int id);

        Task<SuccessResponseDTO> Renew(int ID, decimal coverage, DateOnly expirationDate);

        Task<CreateResponseDTO> AddAsync(Insurance insurance);

        Task<SuccessResponseDTO> UpdateAsync(Insurance insurance);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}