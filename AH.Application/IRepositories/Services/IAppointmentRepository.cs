using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IAppointmentRepository : IService
    {
        Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllAsync(AppointmentFilterDTO filterDTO);

        Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllByDoctorIDAsync(int doctorID);

        Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllByPatientIDAsync(int patientID);

        Task<GetByIDResponseDTO<AppointmentDTO>> GetByIDAsync(int id);

        Task<CreateResponseDTO> AddAsync(Appointment appointment);

        Task<CreateResponseDTO> AddFromPreviousAppointmentAsync(CreateAppointmentFromPreviousDTO app);

        Task<SuccessResponseDTO> UpdateAsync(Appointment appointment);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}