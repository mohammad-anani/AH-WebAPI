using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface IPatientRepository
    {
        Task<GetAllResponseDTO<PatientRowDTO>> GetAllAsync(PatientFilterDTO filterDTO);

        Task<GetAllResponseDTO<PatientRowDTO>> GetAllForDoctorAsync(int doctorID, PatientFilterDTO filterDTO);

        Task<GetByIDResponseDTO<PatientDTO>> GetByIDAsync(int id);

        Task<int> AddAsync(Patient patient);

        Task<bool> UpdateAsync(Patient patient);

        Task<bool> DeleteAsync(int id);
    }
}