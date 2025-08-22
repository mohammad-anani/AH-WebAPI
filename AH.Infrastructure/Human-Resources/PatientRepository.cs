using AH.Domain.Entities;
using AH.Application.Repositories;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Application.DTOs.Row;

namespace AH.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public async Task<Tuple<IEnumerable<PatientRowDTO>, int>> GetAllAsync(PatientFilterDTO filterDTO)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Tuple<IEnumerable<PatientRowDTO>, int>> GetForDoctorAsync(int doctorID)
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