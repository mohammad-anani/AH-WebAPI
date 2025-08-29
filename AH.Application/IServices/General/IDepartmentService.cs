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
    /// Service interface for Department business operations.
    /// Provides a business layer abstraction over department repository operations.
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// Retrieves a paginated list of departments based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for department search</param>
        /// <returns>ServiceResult containing department row DTOs and count as tuple</returns>
        Task<ServiceResult<(IEnumerable<DepartmentRowDTO> items, int count)>> GetAllAsync(DepartmentFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific department by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the department</param>
        /// <returns>ServiceResult containing department DTO with complete information or null if not found</returns>
        Task<ServiceResult<DepartmentDTO>> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new department in the system.
        /// </summary>
        /// <param name="createDepartmentDTO">The department create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created department</returns>
        Task<ServiceResult<int>> AddAsync(CreateDepartmentDTO createDepartmentDTO);

        /// <summary>
        /// Updates an existing department's information.
        /// </summary>
        /// <param name="updateDepartmentDTO">The department update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdateDepartmentDTO updateDepartmentDTO);

        /// <summary>
        /// Deletes a department from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the department to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}