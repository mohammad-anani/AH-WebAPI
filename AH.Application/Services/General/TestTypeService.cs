using AH.Application.DTOs.Create;
using AH.Application.DTOs.Update;
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
    /// Service implementation for TestType business operations.
    /// Acts as a business layer wrapper around the test type repository.
    /// </summary>
    public class TestTypeService : ITestTypeService
    {
        private readonly ITestTypeRepository _testTypeRepository;

        /// <summary>
        /// Initializes a new instance of the TestTypeService.
        /// </summary>
        /// <param name="testTypeRepository">The test type repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public TestTypeService(ITestTypeRepository testTypeRepository)
        {
            _testTypeRepository = testTypeRepository ?? throw new ArgumentNullException(nameof(testTypeRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of test types based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for test type search</param>
        /// <returns>Response containing test type row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<TestTypeRowDTO>> GetAllAsync(TestTypeFilterDTO filterDTO)
        {
            var response = await _testTypeRepository.GetAllAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve test types.", response.Exception);
            }

            return new GetAllResponseDataDTO<TestTypeRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a specific test type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test type</param>
        /// <returns>TestType DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<TestTypeDTO?> GetByIDAsync(int id)
        {
            var response = await _testTypeRepository.GetByIDAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve test type with ID {id}.", response.Exception);
            }

            return response.Item;
        }

        /// <summary>
        /// Creates a new test type in the system.
        /// </summary>
        /// <param name="createTestTypeDTO">The test type create DTO containing creation information</param>
        /// <returns>The ID of the newly created test type</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddAsync(CreateTestTypeDTO createTestTypeDTO)
        {
            var testType = createTestTypeDTO.ToTestType();
            var response = await _testTypeRepository.AddAsync(testType);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create test type.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Updates an existing test type's information.
        /// </summary>
        /// <param name="updateTestTypeDTO">The test type update DTO with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> UpdateAsync(UpdateTestTypeDTO updateTestTypeDTO)
        {
            var testType = updateTestTypeDTO.ToTestType();
            var response = await _testTypeRepository.UpdateAsync(testType);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to update test type with ID {updateTestTypeDTO.ID}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Deletes a test type from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the test type to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _testTypeRepository.DeleteAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to delete test type with ID {id}.", response.Exception);
            }

            return response.Success;
        }
    }
}