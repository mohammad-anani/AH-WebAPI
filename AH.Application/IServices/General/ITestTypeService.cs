using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

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
        /// <returns>Response containing test type row DTOs and count</returns>
        Task<GetAllResponseDataDTO<TestTypeRowDTO>> GetAllAsync(TestTypeFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific test type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test type</param>
        /// <returns>TestType DTO with complete information or null if not found</returns>
        Task<TestTypeDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new test type in the system.
        /// </summary>
        /// <param name="createTestTypeDTO">The test type create DTO containing creation information</param>
        /// <returns>The ID of the newly created test type</returns>
        Task<int> AddAsync(CreateTestTypeDTO createTestTypeDTO);

        /// <summary>
        /// Updates an existing test type's information.
        /// </summary>
        /// <param name="testType">The test type entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(TestType testType);

        /// <summary>
        /// Deletes a test type from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the test type to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(int id);
    }
}