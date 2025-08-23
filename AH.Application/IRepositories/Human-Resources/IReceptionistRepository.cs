using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IReceptionistRepository : IEmployee
    {
        Task<ListResponseDTO<ReceptionistRowDTO>> GetAllAsync(ReceptionistFilterDTO filterDTO);

        Task<Receptionist> GetByIdAsync(int id);

        Task<int> AddAsync(Receptionist receptionist);

        Task<bool> UpdateAsync(Receptionist receptionist);

        Task<bool> DeleteAsync(int id);
    }
}