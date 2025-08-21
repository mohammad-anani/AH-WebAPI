using AH.Domain.Entities;
using AH.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        public async Task<IEnumerable<Operation>> GetAllAsync()
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Operation> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Operation operation)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Operation operation)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        Task<IEnumerable<Operation>> IOperationRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Appointment>> IOperationRepository.GetAllByDoctorIDAsync(int doctorID)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Appointment>> IOperationRepository.GetAllByPatientIDAsync(int patientID)
        {
            throw new NotImplementedException();
        }

        Task<Operation> IOperationRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<int> IOperationRepository.AddAsync(Operation operation)
        {
            throw new NotImplementedException();
        }

        Task<bool> IOperationRepository.UpdateAsync(Operation operation)
        {
            throw new NotImplementedException();
        }

        Task<bool> IOperationRepository.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IService.StartAsync(int id, string? notes)
        {
            throw new NotImplementedException();
        }

        Task<bool> IService.CancelAsync(int id, string? notes)
        {
            throw new NotImplementedException();
        }

        Task<bool> IService.CompleteAsync(int id, string? notes, string result)
        {
            throw new NotImplementedException();
        }

        Task<bool> IService.RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            throw new NotImplementedException();
        }
    }
}