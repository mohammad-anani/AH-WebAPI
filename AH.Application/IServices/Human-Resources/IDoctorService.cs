using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;

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
        /// <returns>ServiceResult containing doctor row DTOs and count as tuple</returns>
        Task<ServiceResult<GetAllResponseDataDTO<DoctorRowDTO>>> GetAllAsync(DoctorFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific doctor by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor</param>
        /// <returns>ServiceResult containing doctor DTO with complete information or null if not found</returns>
        Task<ServiceResult<DoctorDTO>> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new doctor in the system.
        /// </summary>
        /// <param name="createDoctorDTO">The doctor create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created doctor</returns>
        Task<ServiceResult<int>> AddAsync(CreateDoctorDTO createDoctorDTO);

        /// <summary>
        /// Updates an existing doctor's information.
        /// </summary>
        /// <param name="updateDoctorDTO">The doctor update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdateDoctorDTO updateDoctorDTO);

        /// <summary>
        /// Deletes a doctor from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}