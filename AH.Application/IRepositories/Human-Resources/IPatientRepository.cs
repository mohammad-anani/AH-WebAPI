using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IPatientRepository
    {
        Task<GetAllResponseDTO<PatientRowDTO>> GetAllAsync(PatientFilterDTO filterDTO);

        Task<GetAllResponseDTO<PatientRowDTO>> GetAllForDoctorAsync(int doctorID, PatientFilterDTO filterDTO);

        Task<GetByIDResponseDTO<PatientDTO>> GetByIDAsync(int id);

        Task<CreateResponseDTO> AddAsync(Patient patient);

        Task<SuccessResponseDTO> UpdateAsync(Patient patient);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}