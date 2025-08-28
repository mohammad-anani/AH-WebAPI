using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Doctor business operations.
    /// Provides a business layer abstraction over doctor repository operations.
    /// </summary>
    public interface IDoctorService : IEmployee
    {
        /// <summary>
        /// Retrieves a paginated list of doctors based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for doctor search</param>
        /// <returns>Response containing doctor row DTOs and count</returns>
        Task<GetAllResponseDataDTO<DoctorRowDTO>> GetAllAsync(DoctorFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific doctor by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor</param>
        /// <returns>Doctor DTO with complete information or null if not found</returns>
        Task<DoctorDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new doctor in the system.
        /// </summary>
        /// <param name="doctor">The doctor entity to create</param>
        /// <returns>The ID of the newly created doctor</returns>
        Task<int> AddAsync(Doctor doctor);

        /// <summary>
        /// Updates an existing doctor's information.
        /// </summary>
        /// <param name="doctor">The doctor entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(Doctor doctor);

        /// <summary>
        /// Deletes a doctor from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(int id);
    }
}