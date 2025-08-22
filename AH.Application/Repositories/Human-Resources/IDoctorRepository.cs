using AH.Domain.Entities;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IDoctorRepository:IEmployee
    {
        Task<Tuple<IEnumerable<Doctor>, int>> GetAllAsync(DoctorFilterDTO filterDTO);
        Task<Doctor> GetByIdAsync(int id);
        Task<int> AddAsync(Doctor doctor);
        Task<bool> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(int id);
    }
}