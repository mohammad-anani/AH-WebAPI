using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();

        Task<IEnumerable<Patient>> GetForDoctorAsync(int doctorID);

        Task<Patient> GetByIdAsync(int id);
        Task<int> AddAsync(Patient patient);
        Task<bool> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(int id);
    }
}
