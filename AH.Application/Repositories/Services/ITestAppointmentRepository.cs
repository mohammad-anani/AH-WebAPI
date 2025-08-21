using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface ITestAppointmentRepository:IService
    {
        Task<IEnumerable<TestAppointment>> GetAllAsync();

        Task<IEnumerable<Appointment>> GetAllByPatientIDAsync(int patientID);
        Task<TestAppointment> GetByIdAsync(int id);
        Task<int> AddAsync(TestAppointment testAppointment);

        Task<int> AddFromTestOrderAsync(TestAppointment testAppointment);

        Task<bool> UpdateAsync(TestAppointment testAppointment);
        Task<bool> DeleteAsync(int id);
    }
}