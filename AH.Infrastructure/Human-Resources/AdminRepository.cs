using AH.Domain.Entities;
using AH.Application.Repositories;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using AH.Application.DTOs.Row;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public async Task<Tuple<IEnumerable<AdminRowDTO>, int>> GetAllAsync(AdminFilterDTO filterDTO)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Admin> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Admin admin)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Admin admin)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> LeaveAsync(int employeeID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}