using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Receptionist business operations.
    /// Provides a business layer abstraction over receptionist repository operations.
    /// </summary>
    public interface IReceptionistService : IEmployee
    {
        /// <summary>
        /// Retrieves a paginated list of receptionists based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for receptionist search</param>
        /// <returns>Response containing receptionist row DTOs and count</returns>
        Task<ServiceResult<(IEnumerable<ReceptionistRowDTO> items, int count)>> GetAllAsync(ReceptionistFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific receptionist by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the receptionist</param>
        /// <returns>Receptionist DTO with complete information or null if not found</returns>
        Task<ReceptionistDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new receptionist in the system.
        /// </summary>
        /// <param name="createReceptionistDTO">The receptionist create DTO containing creation information</param>
        /// <returns>The ID of the newly created receptionist</returns>
        Task<ServiceResult<int>> AddAsync(CreateReceptionistDTO createReceptionistDTO);

        /// <summary>
        /// Updates an existing receptionist's information.
        /// </summary>
        /// <param name="receptionist">The receptionist entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(Receptionist receptionist);

        /// <summary>
        /// Deletes a receptionist from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the receptionist to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}