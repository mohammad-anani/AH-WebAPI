using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public async Task<(IEnumerable<PatientRowDTO> Items, int Count)> GetAllAsync(PatientFilterDTO filterDTO)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<PatientRowDTO> Items, int Count)> GetForDoctorAsync(int doctorID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Patient patient)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}