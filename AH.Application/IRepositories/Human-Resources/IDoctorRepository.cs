using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IDoctorRepository : IEmployee
    {
        Task<(IEnumerable<DoctorRowDTO> Items, int Count)> GetAllAsync(DoctorFilterDTO filterDTO);

        Task<Doctor> GetByIdAsync(int id);

        Task<int> AddAsync(Doctor doctor);

        Task<bool> UpdateAsync(Doctor doctor);

        Task<bool> DeleteAsync(int id);
    }
}