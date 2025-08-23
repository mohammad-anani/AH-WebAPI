using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IInsuranceRepository
    {
        Task<ListResponseDTO<InsuranceRowDTO>> GetAllByPatientIDAsync(int patiendID);

        Task<Insurance> GetByIdAsync(int id);

        Task<bool> Renew(int id);

        Task<int> AddAsync(Insurance insurance);

        Task<bool> UpdateAsync(Insurance insurance);

        Task<bool> DeleteAsync(int id);
    }
}