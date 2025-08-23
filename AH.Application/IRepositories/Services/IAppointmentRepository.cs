using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Application

{
    public interface IAppointmentRepository : IService
    {
        Task<(IEnumerable<AppointmentRowDTO> Items, int Count)> GetAllAsync(AppointmentFilterDTO filterDTO);

        Task<(IEnumerable<AppointmentRowDTO> Items, int Count)> GetAllByDoctorIDAsync(int doctorID);

        Task<(IEnumerable<AppointmentRowDTO> Items, int Count)> GetAllByPatientIDAsync(int patientID);

        Task<Appointment> GetByIdAsync(int id);

        Task<int> AddAsync(Appointment appointment);

        Task<int> AddFromPreviousAppointmentAsync(Appointment appointment);

        Task<bool> UpdateAsync(Appointment appointment);

        Task<bool> DeleteAsync(int id);
    }
}