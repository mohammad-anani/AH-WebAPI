using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface IDoctorRepository : IEmployee
    {
        Task<GetAllResponseDTO<DoctorRowDTO>> GetAllAsync(DoctorFilterDTO filterDTO);

        Task<GetByIDResponseDTO<DoctorDTO>> GetByIDAsync(int id);

        Task<CreateResponseDTO> AddAsync(Doctor doctor);

        Task<SuccessResponseDTO> UpdateAsync(Doctor doctor);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}