using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Application

{
    public interface IAppointmentRepository : IService
    {
        Task<ListResponseDTO<AppointmentRowDTO>> GetAllAsync(AppointmentFilterDTO filterDTO);

        Task<ListResponseDTO<AppointmentRowDTO>> GetAllByDoctorIDAsync(int doctorID);

        Task<ListResponseDTO<AppointmentRowDTO>> GetAllByPatientIDAsync(int patientID);

        Task<Appointment> GetByIdAsync(int id);

        Task<int> AddAsync(Appointment appointment);

        Task<int> AddFromPreviousAppointmentAsync(Appointment appointment);

        Task<bool> UpdateAsync(Appointment appointment);

        Task<bool> DeleteAsync(int id);
    }
}