using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IAdminRepository:IEmployee
    {
        Task<IEnumerable<Admin>> GetAllAsync();
        Task<Admin> GetByIdAsync(int id);
        Task<int> AddAsync(Admin admin);
        Task<bool> UpdateAsync(Admin admin);
        Task<bool> DeleteAsync(int id);
    }
}