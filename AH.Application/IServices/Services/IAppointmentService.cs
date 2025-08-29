using AH.Application.DTOs.Create;
using AH.Application.DTOs.Update;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

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
        /// <returns>Response containing appointment row DTOs and count</returns>
        Task<GetAllResponseDataDTO<AppointmentRowDTO>> GetAllAsync(AppointmentFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a paginated list of appointments for a specific doctor.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <returns>Response containing appointment row DTOs and count</returns>
        Task<GetAllResponseDataDTO<AppointmentRowDTO>> GetAllByDoctorIDAsync(int doctorID);

        /// <summary>
        /// Retrieves a paginated list of appointments for a specific patient.
        /// </summary>
        /// <param name="patientID">The unique identifier of the patient</param>
        /// <returns>Response containing appointment row DTOs and count</returns>
        Task<GetAllResponseDataDTO<AppointmentRowDTO>> GetAllByPatientIDAsync(int patientID);

        /// <summary>
        /// Retrieves a specific appointment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment</param>
        /// <returns>Appointment DTO with complete information or null if not found</returns>
        Task<AppointmentDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new appointment in the system.
        /// </summary>
        /// <param name="createAppointmentDTO">The appointment create DTO containing creation information</param>
        /// <returns>The ID of the newly created appointment</returns>
        Task<int> AddAsync(CreateAppointmentDTO createAppointmentDTO);

        /// <summary>
        /// Creates a new appointment from a previous appointment.
        /// </summary>
        /// <param name="createDTO">The DTO containing previous appointment information and new appointment details</param>
        /// <returns>The ID of the newly created appointment</returns>
        Task<int> AddFromPreviousAppointmentAsync(CreateAppointmentFromPreviousDTO createDTO);

        /// <summary>
        /// Updates an existing appointment's information.
        /// </summary>
        /// <param name="updateAppointmentDTO">The appointment update DTO with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(UpdateAppointmentDTO updateAppointmentDTO);

        /// <summary>
        /// Deletes an appointment from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(int id);
    }
}