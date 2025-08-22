using AH.Domain.Entities;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Application.DTOs.Row;

namespace AH.Application.Repositories
{
    public interface IAdminRepository:IEmployee
    {
        Task<Tuple<IEnumerable<AdminRowDTO>, int>> GetAllAsync(AdminFilterDTO filterDTO);
        Task<Admin> GetByIdAsync(int id);
        Task<int> AddAsync(Admin admin);
        Task<bool> UpdateAsync(Admin admin);
        Task<bool> DeleteAsync(int id);
    }
}