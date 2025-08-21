using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IDoctorRepository:IEmployee
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(int id);
        Task<int> AddAsync(Doctor doctor);
        Task<bool> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(int id);
    }
}