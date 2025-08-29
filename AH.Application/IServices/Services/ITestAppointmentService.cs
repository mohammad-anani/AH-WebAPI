using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for TestAppointment business operations.
    /// Provides a business layer abstraction over test appointment repository operations.
    /// </summary>
    public interface ITestAppointmentService : IService
    {
        /// <summary>
        /// Retrieves a paginated list of test appointments based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for test appointment search</param>
        /// <returns>Response containing test appointment row DTOs and count</returns>
        Task<ServiceResult<(IEnumerable<TestAppointmentRowDTO> items, int count)>> GetAllAsync(TestAppointmentFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a paginated list of test appointments for a specific patient.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for test appointment search including patient ID</param>
        /// <returns>Response containing test appointment row DTOs and count</returns>
        Task<ServiceResult<(IEnumerable<TestAppointmentRowDTO> items, int count)>> GetAllByPatientIDAsync(TestAppointmentFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific test appointment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test appointment</param>
        /// <returns>TestAppointment DTO with complete information or null if not found</returns>
        Task<TestAppointmentDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new test appointment in the system.
        /// </summary>
        /// <param name="createTestAppointmentDTO">The test appointment create DTO containing creation information</param>
        /// <returns>The ID of the newly created test appointment</returns>
        Task<ServiceResult<int>> AddAsync(CreateTestAppointmentDTO createTestAppointmentDTO);

        /// <summary>
        /// Creates a new test appointment from an existing test order.
        /// </summary>
        /// <param name="createDTO">The DTO containing test order information and appointment details</param>
        /// <returns>The ID of the newly created test appointment</returns>
        Task<ServiceResult<int>> AddFromTestOrderAsync(CreateTestAppointmentFromTestOrderDTO createDTO);

        /// <summary>
        /// Updates an existing test appointment's information.
        /// </summary>
        /// <param name="testAppointment">The test appointment entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(TestAppointment testAppointment);

        /// <summary>
        /// Deletes a test appointment from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the test appointment to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}