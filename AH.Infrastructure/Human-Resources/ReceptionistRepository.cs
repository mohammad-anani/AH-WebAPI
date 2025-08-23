using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Infrastructure.Repositories
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        public async Task<ListResponseDTO<ReceptionistRowDTO>> GetAllAsync(ReceptionistFilterDTO filterDTO)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Receptionist> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Receptionist receptionist)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Receptionist receptionist)
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