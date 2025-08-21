using AH.Domain.Entities;
using AH.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Repositories
{
    public class TestAppointmentRepository : ITestAppointmentRepository
    {
        public async Task<IEnumerable<TestAppointment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TestAppointment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(TestAppointment testAppointment)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TestAppointment testAppointment)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Appointment>> ITestAppointmentRepository.GetAllByPatientIDAsync(int patientID)
        {
            throw new NotImplementedException();
        }

        Task<int> ITestAppointmentRepository.AddFromTestOrderAsync(TestAppointment testAppointment)
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