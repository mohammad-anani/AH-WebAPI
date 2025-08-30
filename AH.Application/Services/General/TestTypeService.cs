using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;
using AH.Application.IRepositories;
using AH.Application.IServices;

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
        /// <returns>ServiceResult containing test type row DTOs and count as tuple</returns>
        public async Task<ServiceResult<GetAllResponseDataDTO<TestTypeRowDTO>>> GetAllAsync(TestTypeFilterDTO filterDTO)
        {
            var response = await _testTypeRepository.GetAllAsync(filterDTO);
            var data = new GetAllResponseDataDTO<TestTypeRowDTO>(response);
            return ServiceResult<GetAllResponseDataDTO<TestTypeRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a specific test type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test type</param>
        /// <returns>ServiceResult containing test type DTO with complete information or null if not found</returns>
        public async Task<ServiceResult<TestTypeDTO>> GetByIDAsync(int id)
        {
            var response = await _testTypeRepository.GetByIDAsync(id);
            return ServiceResult<TestTypeDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new test type in the system.
        /// </summary>
        /// <param name="createTestTypeDTO">The test type create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created test type</returns>
        public async Task<ServiceResult<int>> AddAsync(CreateTestTypeDTO createTestTypeDTO)
        {
            var testType = createTestTypeDTO.ToTestType();
            var response = await _testTypeRepository.AddAsync(testType);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing test type's information.
        /// </summary>
        /// <param name="updateTestTypeDTO">The test type update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> UpdateAsync(UpdateTestTypeDTO updateTestTypeDTO)
        {
            var testType = updateTestTypeDTO.ToTestType();
            var response = await _testTypeRepository.UpdateAsync(testType);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes a test type from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the test type to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _testTypeRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}