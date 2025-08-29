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
    /// Service interface for Patient business operations.
    /// Provides a business layer abstraction over patient repository operations.
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Retrieves a paginated list of patients based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for patient search</param>
        /// <returns>ServiceResult containing patient row DTOs and count as tuple</returns>
        Task<ServiceResult<(IEnumerable<PatientRowDTO> items, int count)>> GetAllAsync(PatientFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a paginated list of patients for a specific doctor based on filter criteria.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <param name="filterDTO">Filter criteria for patient search</param>
        /// <returns>ServiceResult containing patient row DTOs and count as tuple</returns>
        Task<ServiceResult<(IEnumerable<PatientRowDTO> items, int count)>> GetAllForDoctorAsync(int doctorID, PatientFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific patient by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the patient</param>
        /// <returns>ServiceResult containing patient DTO with complete information or null if not found</returns>
        Task<ServiceResult<PatientDTO?>> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new patient in the system.
        /// </summary>
        /// <param name="createPatientDTO">The patient create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created patient</returns>
        Task<ServiceResult<int>> AddAsync(CreatePatientDTO createPatientDTO);

        /// <summary>
        /// Updates an existing patient's information.
        /// </summary>
        /// <param name="updatePatientDTO">The patient update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdatePatientDTO updatePatientDTO);

        /// <summary>
        /// Deletes a patient from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the patient to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}