using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Infrastructure.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        public async Task<int> AddAsync(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ListResponseDTO<PrescriptionRowDTO>> GetAllByAppointmentIDAsync(int appointmentID)
        {
            throw new NotImplementedException();
        }

        public async Task<Prescription> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Prescription prescription)
        {
            throw new NotImplementedException();
        }
    }
}