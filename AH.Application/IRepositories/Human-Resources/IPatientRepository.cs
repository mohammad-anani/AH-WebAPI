using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IPatientRepository
    {
        Task<ListResponseDTO<PatientRowDTO>> GetAllAsync(PatientFilterDTO filterDTO);

        Task<ListResponseDTO<PatientRowDTO>> GetForDoctorAsync(int doctorID);

        Task<Patient> GetByIdAsync(int id);

        Task<int> AddAsync(Patient patient);

        Task<bool> UpdateAsync(Patient patient);

        Task<bool> DeleteAsync(int id);
    }
}