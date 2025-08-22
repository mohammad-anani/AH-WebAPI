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
    public interface IReceptionistRepository:IEmployee
    {
        Task<Tuple<IEnumerable<ReceptionistRowDTO>, int>> GetAllAsync(ReceptionistFilterDTO filterDTO);
        Task<Receptionist> GetByIdAsync(int id);
        Task<int> AddAsync(Receptionist receptionist);
        Task<bool> UpdateAsync(Receptionist receptionist);
        Task<bool> DeleteAsync(int id);
    }
}