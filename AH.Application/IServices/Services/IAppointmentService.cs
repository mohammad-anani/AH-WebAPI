using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Appointment business operations.
    /// Provides a business layer abstraction over appointment repository operations.
    /// </summary>
    public interface IAppointmentService : IService
    {
        /// <summary>
        /// Retrieves a paginated list of appointments based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for appointment search</param>
        /// <returns>ServiceResult containing appointment row DTOs and count as tuple</returns>
        Task<ServiceResult<GetAllResponseDataDTO<AppointmentRowDTO>>> GetAllAsync(AppointmentFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a paginated list of appointments for a specific doctor.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <returns>ServiceResult containing appointment row DTOs and count as tuple</returns>
        Task<ServiceResult<GetAllResponseDataDTO<AppointmentRowDTO>>> GetAllByDoctorIDAsync(AppointmentFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a paginated list of appointments for a specific patient.
        /// </summary>
        /// <param name="patientID">The unique identifier of the patient</param>
        /// <returns>ServiceResult containing appointment row DTOs and count as tuple</returns>
        Task<ServiceResult<GetAllResponseDataDTO<AppointmentRowDTO>>> GetAllByPatientIDAsync(AppointmentFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific appointment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment</param>
        /// <returns>ServiceResult containing appointment DTO with complete information or null if not found</returns>
        Task<ServiceResult<AppointmentDTO>> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new appointment in the system.
        /// </summary>
        /// <param name="createAppointmentDTO">The appointment create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created appointment</returns>
        Task<ServiceResult<int>> AddAsync(CreateAppointmentDTO createAppointmentDTO);

        /// <summary>
        /// Creates a new appointment from a previous appointment.
        /// </summary>
        /// <param name="createDTO">The DTO containing previous appointment information and new appointment details</param>
        /// <returns>ServiceResult containing the ID of the newly created appointment</returns>
        Task<ServiceResult<int>> AddFromPreviousAppointmentAsync(CreateAppointmentFromPreviousDTO createDTO);

        /// <summary>
        /// Updates an existing appointment's information.
        /// </summary>
        /// <param name="updateAppointmentDTO">The appointment update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdateAppointmentDTO updateAppointmentDTO);

        Task<ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>> GetPaymentsAsync(ServicePaymentsDTO filterDTO);

        /// <summary>
        /// Deletes an appointment from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);

        /// <summary>
        /// Starts an appointment, indicating that the service is in progress.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to start</param>
        /// <param name="notes">Optional notes or comments about the appointment</param>
        /// <returns>ServiceResult containing true if the operation was successful, false otherwise</returns>
        Task<ServiceResult<bool>> StartAsync(int id, string? notes);

        /// <summary>
        /// Cancels an appointment, marking it as not attended.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to cancel</param>
        /// <param name="notes">Optional notes or comments about the cancellation</param>
        /// <returns>ServiceResult containing true if the operation was successful, false otherwise</returns>
        Task<ServiceResult<bool>> CancelAsync(int id, string? notes);

        /// <summary>
        /// Marks an appointment as complete, indicating that the service has been provided.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to complete</param>
        /// <param name="notes">Optional notes or comments about the appointment</param>
        /// <param name="result">The result or outcome of the appointment</param>
        /// <returns>ServiceResult containing true if the operation was successful, false otherwise</returns>
        Task<ServiceResult<bool>> CompleteAsync(int id, string? notes, string result);

        /// <summary>
        /// Marks an appointment as complete, indicating that the service has been provided.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to complete</param>
        /// <param name="notes">Optional notes or comments about the appointment</param>
        /// <param name="result">The result or outcome of the appointment</param>
        /// <param name="testTypeIDs">A collection of related test type IDs to be passed to the stored procedure</param>
        /// <returns>ServiceResult containing true if the operation was successful, false otherwise</returns>
        Task<ServiceResult<bool>> CompleteAsync(int id, string? notes, string result, IEnumerable<int>? testTypeIDs);

        /// <summary>
        /// Reschedules an appointment to a new date and time.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to reschedule</param>
        /// <param name="notes">Optional notes or comments about the rescheduling</param>
        /// <param name="newScheduledDate">The new date and time for the appointment</param>
        /// <returns>ServiceResult containing true if the operation was successful, false otherwise</returns>
        Task<ServiceResult<bool>> RescheduleAsync(int id, string? notes, DateTime newScheduledDate);

        /// <summary>
        /// Processes a payment for an appointment.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment for which to process the payment</param>
        /// <param name="dto">The service payment DTO containing payment information</param>
        /// <returns>ServiceResult containing the ID of the newly created payment record</returns>
        Task<ServiceResult<int>> PayAsync(int id, CreateServicePaymentDTO dto);
    }
}