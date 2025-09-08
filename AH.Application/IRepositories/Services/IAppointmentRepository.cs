using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IAppointmentRepository
    {
        Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllAsync(AppointmentFilterDTO filterDTO);

        Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllByDoctorIDAsync(AppointmentFilterDTO filterDTO);

        Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllByPatientIDAsync(AppointmentFilterDTO filterDTO);

        Task<GetAllResponseDTO<PaymentRowDTO>> GetPaymentsAsync(ServicePaymentsDTO filterDTO);

        Task<GetByIDResponseDTO<AppointmentDTO>> GetByIDAsync(int id);

        Task<CreateResponseDTO> AddAsync(Appointment appointment);

        Task<CreateResponseDTO> AddFromPreviousAppointmentAsync(CreateAppointmentFromPreviousDTO app);

        Task<SuccessResponseDTO> UpdateAsync(Appointment appointment);

        Task<DeleteResponseDTO> DeleteAsync(int id);

        Task<CreateResponseDTO> PayAsync(int appointmentID, int amount, string method, int createdByReceptionistID);

        Task<SuccessResponseDTO> StartAsync(int id, string? notes);

        Task<SuccessResponseDTO> CancelAsync(int id, string? notes);

        // Legacy signature
        Task<SuccessResponseDTO> CompleteAsync(int id, string? notes, string result);

        // Extended signature with test orders CSV
        Task<SuccessResponseDTO> CompleteAsync(int id, string? notes, string result, string? testOrdersCsv);

        Task<SuccessResponseDTO> RescheduleAsync(int id, string? notes, DateTime newScheduledDate);
    }
}