using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;
using AH.Application.IRepositories;
using AH.Application.IServices;

namespace AH.Application.Services
{
    /// <summary>
    /// Service implementation for Appointment business operations.
    /// Acts as a business layer wrapper around the appointment repository.
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        /// <summary>
        /// Initializes a new instance of the AppointmentService.
        /// </summary>
        /// <param name="appointmentRepository">The appointment repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of appointments based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for appointment search</param>
        /// <returns>ServiceResult containing appointment row DTOs and count as tuple</returns>
        public async Task<ServiceResult<GetAllResponseDataDTO<AppointmentRowDTO>>> GetAllAsync(AppointmentFilterDTO filterDTO)
        {
            var response = await _appointmentRepository.GetAllAsync(filterDTO);
            var data = new GetAllResponseDataDTO<AppointmentRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<AppointmentRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a paginated list of appointments for a specific doctor.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <returns>ServiceResult containing appointment row DTOs and count as tuple</returns>
        public async Task<ServiceResult<GetAllResponseDataDTO<AppointmentRowDTO>>> GetAllByDoctorIDAsync(AppointmentFilterDTO filterDTO)
        {
            var response = await _appointmentRepository.GetAllByDoctorIDAsync(filterDTO);
            var data = new GetAllResponseDataDTO<AppointmentRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<AppointmentRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a paginated list of appointments for a specific patient.
        /// </summary>
        /// <param name="patientID">The unique identifier of the patient</param>
        /// <returns>ServiceResult containing appointment row DTOs and count as tuple</returns>
        public async Task<ServiceResult<GetAllResponseDataDTO<AppointmentRowDTO>>> GetAllByPatientIDAsync(AppointmentFilterDTO filterDTO)
        {
            var response = await _appointmentRepository.GetAllByPatientIDAsync(filterDTO);
            var data = new GetAllResponseDataDTO<AppointmentRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<AppointmentRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a specific appointment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment</param>
        /// <returns>ServiceResult containing appointment DTO with complete information or null if not found</returns>
        public async Task<ServiceResult<AppointmentDTO>> GetByIDAsync(int id)
        {
            var response = await _appointmentRepository.GetByIDAsync(id);
            return ServiceResult<AppointmentDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new appointment in the system.
        /// </summary>
        /// <param name="createAppointmentDTO">The appointment create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created appointment</returns>
        public async Task<ServiceResult<int>> AddAsync(CreateAppointmentDTO createAppointmentDTO)
        {
            var appointment = createAppointmentDTO.ToAppointment();
            var response = await _appointmentRepository.AddAsync(appointment);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Creates a new appointment from a previous appointment.
        /// </summary>
        /// <param name="createDTO">The DTO containing previous appointment information and new appointment details</param>
        /// <returns>ServiceResult containing the ID of the newly created appointment</returns>
        public async Task<ServiceResult<int>> AddFromPreviousAppointmentAsync(CreateAppointmentFromPreviousDTO createDTO)
        {
            var response = await _appointmentRepository.AddFromPreviousAppointmentAsync(createDTO);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing appointment's information.
        /// </summary>
        /// <param name="updateAppointmentDTO">The appointment update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> UpdateAsync(UpdateAppointmentDTO updateAppointmentDTO)
        {
            var appointment = updateAppointmentDTO.ToAppointment();
            var response = await _appointmentRepository.UpdateAsync(appointment);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes an appointment from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _appointmentRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Starts an appointment by updating its status to started.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to start</param>
        /// <param name="notes">Optional notes for the start operation</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> StartAsync(int id, string? notes)
        {
            var response = await _appointmentRepository.StartAsync(id, notes);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Cancels an appointment by updating its status to cancelled.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to cancel</param>
        /// <param name="notes">Optional notes explaining the cancellation reason</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> CancelAsync(int id, string? notes)
        {
            var response = await _appointmentRepository.CancelAsync(id, notes);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Completes an appointment by updating its status to completed.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to complete</param>
        /// <param name="notes">Optional notes for the completion</param>
        /// <param name="result">The result or outcome of the appointment</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> CompleteAsync(int id, string? notes, string result)
        {
            var response = await _appointmentRepository.CompleteAsync(id, notes, result);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Reschedules an appointment to a new date and time.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to reschedule</param>
        /// <param name="notes">Optional notes explaining the reschedule reason</param>
        /// <param name="newScheduledDate">The new scheduled date and time</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            var response = await _appointmentRepository.RescheduleAsync(id, notes, newScheduledDate);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        public async Task<ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>> GetPaymentsAsync(ServicePaymentsDTO filterDTO)
        {
            var response = await _appointmentRepository.GetPaymentsAsync(filterDTO);
            var data = new GetAllResponseDataDTO<PaymentRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>.Create(data, response.Exception); ;
        }
    }
}