using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IReceptionistRepository:IEmployee
    {
        Task<IEnumerable<Receptionist>> GetAllAsync();
        Task<Receptionist> GetByIdAsync(int id);
        Task<int> AddAsync(Receptionist receptionist);
        Task<bool> UpdateAsync(Receptionist receptionist);
        Task<bool> DeleteAsync(int id);
    }
}