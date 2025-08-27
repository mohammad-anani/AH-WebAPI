using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface IReceptionistRepository : IEmployee
    {
        Task<GetAllResponseDTO<ReceptionistRowDTO>> GetAllAsync(ReceptionistFilterDTO filterDTO);

        Task<GetByIDResponseDTO<ReceptionistDTO>> GetByIDAsync(int id);

        Task<int> AddAsync(Receptionist receptionist);

        Task<bool> UpdateAsync(Receptionist receptionist);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}