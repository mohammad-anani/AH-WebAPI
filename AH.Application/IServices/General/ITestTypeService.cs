using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for TestType business operations.
    /// Provides a business layer abstraction over test type repository operations.
    /// </summary>
    public interface ITestTypeService
    {
        /// <summary>
        /// Retrieves a paginated list of test types based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for test type search</param>
        /// <returns>ServiceResult containing test type row DTOs and count</returns>
        Task<ServiceResult<GetAllResponseDataDTO<TestTypeRowDTO>>> GetAllAsync(TestTypeFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific test type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test type</param>
        /// <returns>ServiceResult containing test type DTO with complete information or null if not found</returns>
        Task<ServiceResult<TestTypeDTO>> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new test type in the system.
        /// </summary>
        /// <param name="createTestTypeDTO">The test type create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created test type</returns>
        Task<ServiceResult<int>> AddAsync(CreateTestTypeDTO createTestTypeDTO);

        /// <summary>
        /// Updates an existing test type's information.
        /// </summary>
        /// <param name="updateTestTypeDTO">The test type update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdateTestTypeDTO updateTestTypeDTO);

        /// <summary>
        /// Deletes a test type from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the test type to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}