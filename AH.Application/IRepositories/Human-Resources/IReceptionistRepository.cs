using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IReceptionistRepository : IEmployee
    {
        Task<GetAllResponseDTO<ReceptionistRowDTO>> GetAllAsync(ReceptionistFilterDTO filterDTO);

        Task<GetByIDResponseDTO<Receptionist>> GetByIDAsync(int id);

        Task<int> AddAsync(Receptionist receptionist);

        Task<bool> UpdateAsync(Receptionist receptionist);

        Task<bool> DeleteAsync(int id);
    }
}