using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface ITestAppointmentRepository : IService
    {
        Task<(IEnumerable<TestAppointmentRowDTO> Items, int Count)> GetAllAsync(TestAppointmentFilterDTO filterDTO);

        Task<(IEnumerable<AppointmentRowDTO> Items, int Count)> GetAllByPatientIDAsync(int patientID);

        Task<TestAppointment> GetByIdAsync(int id);

        Task<int> AddAsync(TestAppointment testAppointment);

        Task<int> AddFromTestOrderAsync(TestAppointment testAppointment);

        Task<bool> UpdateAsync(TestAppointment testAppointment);

        Task<bool> DeleteAsync(int id);
    }
}