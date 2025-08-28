using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Department business operations.
    /// Provides a business layer abstraction over department repository operations.
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// Retrieves a paginated list of departments based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for department search</param>
        /// <returns>Enumerable of department row DTOs</returns>
        Task<GetAllResponseDataDTO<DepartmentRowDTO>> GetAllAsync(DepartmentFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific department by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the department</param>
        /// <returns>Department DTO with complete information or null if not found</returns>
        Task<DepartmentDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new department in the system.
        /// </summary>
        /// <param name="department">The department entity to create</param>
        /// <returns>The ID of the newly created department</returns>
        Task<int> AddAsync(Department department);

        /// <summary>
        /// Updates an existing department's information.
        /// </summary>
        /// <param name="department">The department entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(Department department);

        /// <summary>
        /// Deletes a department from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the department to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(int id);
    }
}