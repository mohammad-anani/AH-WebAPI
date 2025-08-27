using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface IPrescriptionRepository
    {
        Task<GetAllResponseDTO<PrescriptionRowDTO>> GetAllByAppointmentIDAsync(PrescriptionFilterDTO filterDTO);

        Task<GetByIDResponseDTO<PrescriptionDTO>> GetByIDAsync(int id);

        Task<int> AddAsync(Prescription prescription);

        Task<bool> UpdateAsync(Prescription prescription);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}