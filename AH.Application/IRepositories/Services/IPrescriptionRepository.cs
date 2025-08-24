using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IPrescriptionRepository
    {
        Task<ListResponseDTO<PrescriptionRowDTO>> GetAllByAppointmentIDAsync(PrescriptionFilterDTO filterDTO);

        Task<Prescription> GetByIDAsync(int id);

        Task<int> AddAsync(Prescription prescription);

        Task<bool> UpdateAsync(Prescription prescription);

        Task<bool> DeleteAsync(int id);
    }
}