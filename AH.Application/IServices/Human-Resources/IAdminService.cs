using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Admin business operations.
    /// Provides a business layer abstraction over admin repository operations.
    /// </summary>
    public interface IAdminService : IEmployee
    {
        /// <summary>
        /// Retrieves a paginated list of admins based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for admin search</param>
        /// <returns>Response containing admin row DTOs and count</returns>
        Task<ServiceResult<GetAllResponseDataDTO<AdminRowDTO>>> GetAllAsync(AdminFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific admin by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the admin</param>
        /// <returns>Admin DTO with complete information or null if not found</returns>
        Task<ServiceResult<AdminDTO>> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new admin in the system.
        /// </summary>
        /// <param name="createAdminDTO">The admin create DTO containing creation information</param>
        /// <returns>The ID of the newly created admin</returns>
        Task<ServiceResult<int>> AddAsync(CreateAdminDTO createAdminDTO);

        /// <summary>
        /// Updates an existing admin's information.
        /// </summary>
        /// <param name="admin">The admin entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdateAdminDTO admin);

        /// <summary>
        /// Deletes an admin from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the admin to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}