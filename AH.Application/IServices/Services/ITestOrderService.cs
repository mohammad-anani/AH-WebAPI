using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for TestOrder business operations.
    /// Provides a business layer abstraction over test order repository operations.
    /// </summary>
    public interface ITestOrderService
    {
        /// <summary>
        /// Retrieves a paginated list of test orders based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for test order search</param>
        /// <returns>Response containing test order row DTOs and count</returns>
        Task<GetAllResponseDataDTO<TestOrderRowDTO>> GetAllAsync(TestOrderFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific test order by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test order</param>
        /// <returns>TestOrder DTO with complete information or null if not found</returns>
        Task<TestOrderDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new test order in the system.
        /// </summary>
        /// <param name="testOrder">The test order entity to create</param>
        /// <returns>The ID of the newly created test order</returns>
        Task<int> AddAsync(TestOrder testOrder);

        /// <summary>
        /// Deletes a test order from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the test order to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(int id);
    }
}