using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Patient business operations.
    /// Provides a business layer abstraction over patient repository operations.
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Retrieves a paginated list of patients based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for patient search</param>
        /// <returns>Response containing patient row DTOs and count</returns>
        Task<GetAllResponseDataDTO<PatientRowDTO>> GetAllAsync(PatientFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a paginated list of patients for a specific doctor based on filter criteria.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <param name="filterDTO">Filter criteria for patient search</param>
        /// <returns>Response containing patient row DTOs and count</returns>
        Task<GetAllResponseDataDTO<PatientRowDTO>> GetAllForDoctorAsync(int doctorID, PatientFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific patient by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the patient</param>
        /// <returns>Patient DTO with complete information or null if not found</returns>
        Task<PatientDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new patient in the system.
        /// </summary>
        /// <param name="createPatientDTO">The patient create DTO containing creation information</param>
        /// <returns>The ID of the newly created patient</returns>
        Task<int> AddAsync(CreatePatientDTO createPatientDTO);

        /// <summary>
        /// Updates an existing patient's information.
        /// </summary>
        /// <param name="patient">The patient entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(Patient patient);

        /// <summary>
        /// Deletes a patient from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the patient to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(int id);
    }
}