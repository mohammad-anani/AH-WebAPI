using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Application.IServices;
using AH.Domain.Entities;

namespace AH.Application.Services
{
    /// <summary>
    /// Service implementation for TestOrder business operations.
    /// Acts as a business layer wrapper around the test order repository.
    /// </summary>
    public class TestOrderService : ITestOrderService
    {
        private readonly ITestOrderRepository _testOrderRepository;

        /// <summary>
        /// Initializes a new instance of the TestOrderService.
        /// </summary>
        /// <param name="testOrderRepository">The test order repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public TestOrderService(ITestOrderRepository testOrderRepository)
        {
            _testOrderRepository = testOrderRepository ?? throw new ArgumentNullException(nameof(testOrderRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of test orders based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for test order search</param>
        /// <returns>Response containing test order row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<(IEnumerable<TestOrderRowDTO> items, int count)>> GetAllAsync(TestOrderFilterDTO filterDTO)
        {
            var response = await _testOrderRepository.GetAllAsync(filterDTO);
            return ServiceResult<(IEnumerable<TestOrderRowDTO>, int)>.Create((response.Items, response.Count), response.Exception);
        }

        /// <summary>
        /// Retrieves a specific test order by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test order</param>
        /// <returns>TestOrder DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<TestOrderDTO>> GetByIDAsync(int id)
        {
            var response = await _testOrderRepository.GetByIDAsync(id);
            return ServiceResult<TestOrderDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new test order in the system.
        /// </summary>
        /// <param name="createTestOrderDTO">The test order create DTO containing creation information</param>
        /// <returns>The ID of the newly created test order</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<int>> AddAsync(CreateTestOrderDTO createTestOrderDTO)
        {
            var testOrder = createTestOrderDTO.ToTestOrder();
            var response = await _testOrderRepository.AddAsync(testOrder);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Deletes a test order from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the test order to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _testOrderRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}