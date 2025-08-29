using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using System.Threading;

namespace AH.Application.IRepositories
{
    public interface IInsuranceRepository
    {
        Task<GetAllResponseDTO<InsuranceRowDTO>> GetAllByPatientIDAsync(InsuranceFilterDTO filterDTO, CancellationToken cancellationToken = default);

        Task<GetByIDResponseDTO<InsuranceDTO>> GetByIDAsync(int id, CancellationToken cancellationToken = default);

        Task<SuccessResponseDTO> Renew(int ID, decimal coverage, DateOnly expirationDate, CancellationToken cancellationToken = default);

        Task<CreateResponseDTO> AddAsync(Insurance insurance, CancellationToken cancellationToken = default);

        Task<SuccessResponseDTO> UpdateAsync(Insurance insurance, CancellationToken cancellationToken = default);

        Task<DeleteResponseDTO> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}