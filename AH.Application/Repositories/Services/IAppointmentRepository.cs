using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IAppointmentRepository:IService
    {
        Task<IEnumerable<Appointment>> GetAllAsync();

        Task<IEnumerable<Appointment>> GetAllByDoctorIDAsync(int doctorID);

        Task<IEnumerable<Appointment>> GetAllByPatientIDAsync(int patientID);

        Task<Appointment> GetByIdAsync(int id);
        Task<int> AddAsync(Appointment appointment);

        Task<int> AddFromPreviousAppointmentAsync(Appointment appointment);

        Task<bool> UpdateAsync(Appointment appointment);
        Task<bool> DeleteAsync(int id);
    }
}