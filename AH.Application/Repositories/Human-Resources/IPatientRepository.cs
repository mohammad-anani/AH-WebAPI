using AH.Domain.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<Tuple<IEnumerable<PatientRowDTO>, int>> GetAllAsync(PatientFilterDTO filterDTO);

        Task<Tuple<IEnumerable<PatientRowDTO>, int>> GetForDoctorAsync(int doctorID);

        Task<Patient> GetByIdAsync(int id);
        Task<int> AddAsync(Patient patient);
        Task<bool> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(int id);
    }
}
