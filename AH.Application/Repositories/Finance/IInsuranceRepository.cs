using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IInsuranceRepository
    {
        Task<IEnumerable<Insurance>> GetAllByPatientIDAsync(int patiendID);
        Task<Insurance> GetByIdAsync(int id);

        Task<bool> Renew(int id);

        Task<int> AddAsync(Insurance insurance);
        Task<bool> UpdateAsync(Insurance insurance);
        Task<bool> DeleteAsync(int id);
    }
}