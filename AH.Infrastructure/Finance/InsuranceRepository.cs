using AH.Domain.Entities;
using AH.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Application.DTOs.Row;

namespace AH.Infrastructure.Repositories
{
    public class InsuranceRepository : IInsuranceRepository
    {
        public async Task<Tuple<IEnumerable<InsuranceRowDTO>, int>> GetAllByPatientIDAsync(int patiendID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Insurance> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> Renew(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Insurance insurance)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Insurance insurance)
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