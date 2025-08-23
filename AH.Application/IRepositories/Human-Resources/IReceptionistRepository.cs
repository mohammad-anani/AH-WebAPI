using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IReceptionistRepository : IEmployee
    {
        Task<(IEnumerable<ReceptionistRowDTO> Items, int Count)> GetAllAsync(ReceptionistFilterDTO filterDTO);

        Task<Receptionist> GetByIdAsync(int id);

        Task<int> AddAsync(Receptionist receptionist);

        Task<bool> UpdateAsync(Receptionist receptionist);

        Task<bool> DeleteAsync(int id);
    }
}