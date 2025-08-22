using AH.Domain.Entities;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Application.DTOs.Row;

namespace AH.Application.Repositories
{
    public interface ITestAppointmentRepository:IService
    {
        Task<Tuple<IEnumerable<TestAppointmentRowDTO>, int>> GetAllAsync(TestAppointmentFilterDTO filterDTO);

        Task<Tuple<IEnumerable<AppointmentRowDTO>, int>> GetAllByPatientIDAsync(int patientID);
        Task<TestAppointment> GetByIdAsync(int id);
        Task<int> AddAsync(TestAppointment testAppointment);

        Task<int> AddFromTestOrderAsync(TestAppointment testAppointment);

        Task<bool> UpdateAsync(TestAppointment testAppointment);
        Task<bool> DeleteAsync(int id);
    }
}