using AH.Domain.Entities;
using AH.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Prescription>> GetAllByAppointmentIDAsync(int appointmentID)
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