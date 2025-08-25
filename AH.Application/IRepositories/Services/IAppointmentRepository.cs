using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IAppointmentRepository : IService
    {
        Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllAsync(AppointmentFilterDTO filterDTO);

        Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllByDoctorIDAsync(int doctorID);

        Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllByPatientIDAsync(int patientID);

        Task<GetByIDResponseDTO<Appointment>> GetByIDAsync(int id);

        Task<int> AddAsync(Appointment appointment);

        Task<int> AddFromPreviousAppointmentAsync(Appointment appointment);

        Task<bool> UpdateAsync(Appointment appointment);

        Task<bool> DeleteAsync(int id);
    }
}