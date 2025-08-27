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

        Task<int> AddAsync(Doctor doctor);

        Task<bool> UpdateAsync(Doctor doctor);

        Task<bool> DeleteAsync(int id);
    }
}