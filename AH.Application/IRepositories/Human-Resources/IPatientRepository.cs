using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IPatientRepository
    {
        Task<(IEnumerable<PatientRowDTO> Items, int Count)> GetAllAsync(PatientFilterDTO filterDTO);

        Task<(IEnumerable<PatientRowDTO> Items, int Count)> GetForDoctorAsync(int doctorID);

        Task<Patient> GetByIdAsync(int id);

        Task<int> AddAsync(Patient patient);

        Task<bool> UpdateAsync(Patient patient);

        Task<bool> DeleteAsync(int id);
    }
}