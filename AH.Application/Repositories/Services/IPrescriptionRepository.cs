using AH.Domain.Entities;
using AH.Application.DTOs.Row;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<Tuple<IEnumerable<PrescriptionRowDTO>, int>> GetAllByAppointmentIDAsync(int appointmentID);
        Task<Prescription> GetByIdAsync(int id);
        Task<int> AddAsync(Prescription prescription);
        Task<bool> UpdateAsync(Prescription prescription);
        Task<bool> DeleteAsync(int id);
    }
}