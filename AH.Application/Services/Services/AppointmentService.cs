using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Application.IServices;
using AH.Domain.Entities;

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
        /// <returns>Response containing appointment row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<AppointmentRowDTO>> GetAllAsync(AppointmentFilterDTO filterDTO)
        {
            var response = await _appointmentRepository.GetAllAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve appointments.", response.Exception);
            }

            return new GetAllResponseDataDTO<AppointmentRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a paginated list of appointments for a specific doctor.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <returns>Response containing appointment row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<AppointmentRowDTO>> GetAllByDoctorIDAsync(int doctorID)
        {
            var response = await _appointmentRepository.GetAllByDoctorIDAsync(doctorID);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve appointments by doctor ID.", response.Exception);
            }

            return new GetAllResponseDataDTO<AppointmentRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a paginated list of appointments for a specific patient.
        /// </summary>
        /// <param name="patientID">The unique identifier of the patient</param>
        /// <returns>Response containing appointment row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<AppointmentRowDTO>> GetAllByPatientIDAsync(int patientID)
        {
            var response = await _appointmentRepository.GetAllByPatientIDAsync(patientID);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve appointments by patient ID.", response.Exception);
            }

            return new GetAllResponseDataDTO<AppointmentRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a specific appointment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment</param>
        /// <returns>Appointment DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<AppointmentDTO?> GetByIDAsync(int id)
        {
            var response = await _appointmentRepository.GetByIDAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve appointment with ID {id}.", response.Exception);
            }

            return response.Item;
        }

        /// <summary>
        /// Creates a new appointment in the system.
        /// </summary>
        /// <param name="createAppointmentDTO">The appointment create DTO containing creation information</param>
        /// <returns>The ID of the newly created appointment</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddAsync(CreateAppointmentDTO createAppointmentDTO)
        {
            var appointment = createAppointmentDTO.ToAppointment();
            var response = await _appointmentRepository.AddAsync(appointment);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create appointment.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Creates a new appointment from a previous appointment.
        /// </summary>
        /// <param name="createDTO">The DTO containing previous appointment information and new appointment details</param>
        /// <returns>The ID of the newly created appointment</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddFromPreviousAppointmentAsync(CreateAppointmentFromPreviousDTO createDTO)
        {
            var response = await _appointmentRepository.AddFromPreviousAppointmentAsync(createDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create appointment from previous appointment.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Updates an existing appointment's information.
        /// </summary>
        /// <param name="appointment">The appointment entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            var response = await _appointmentRepository.UpdateAsync(appointment);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to update appointment with ID {appointment.ID}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Deletes an appointment from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _appointmentRepository.DeleteAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to delete appointment with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Starts an appointment by updating its status to started.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to start</param>
        /// <param name="notes">Optional notes for the start operation</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> StartAsync(int id, string? notes)
        {
            var response = await _appointmentRepository.StartAsync(id, notes);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to start appointment with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Cancels an appointment by updating its status to cancelled.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to cancel</param>
        /// <param name="notes">Optional notes explaining the cancellation reason</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> CancelAsync(int id, string? notes)
        {
            var response = await _appointmentRepository.CancelAsync(id, notes);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to cancel appointment with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Completes an appointment by updating its status to completed.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to complete</param>
        /// <param name="notes">Optional notes for the completion</param>
        /// <param name="result">The result or outcome of the appointment</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> CompleteAsync(int id, string? notes, string result)
        {
            var response = await _appointmentRepository.CompleteAsync(id, notes, result);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to complete appointment with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Reschedules an appointment to a new date and time.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to reschedule</param>
        /// <param name="notes">Optional notes explaining the reschedule reason</param>
        /// <param name="newScheduledDate">The new scheduled date and time</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            var response = await _appointmentRepository.RescheduleAsync(id, notes, newScheduledDate);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to reschedule appointment with ID {id}.", response.Exception);
            }

            return response.Success;
        }
    }
}