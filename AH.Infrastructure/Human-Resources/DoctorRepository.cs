using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        public async Task<ListResponseDTO<DoctorRowDTO>> GetAllAsync(DoctorFilterDTO filterDTO)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Doctor doctor)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> LeaveAsync(int employeeID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}