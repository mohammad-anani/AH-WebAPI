using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Filter.Helpers;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IDoctorRepository : IEmployee
    {
        Task<ListResponseDTO<DoctorRowDTO>> GetAllAsync(DoctorFilterDTO filterDTO);

        Task<Doctor> GetByIDAsync(int id);

        Task<int> AddAsync(Doctor doctor);

        Task<bool> UpdateAsync(Doctor doctor);

        Task<bool> DeleteAsync(int id);
    }
}