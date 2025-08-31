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
    /// Service implementation for TestAppointment business operations.
    /// Acts as a business layer wrapper around the test appointment repository.
    /// </summary>
    public class TestAppointmentService : ITestAppointmentService
    {
        private readonly ITestAppointmentRepository _testAppointmentRepository;

        /// <summary>
        /// Initializes a new instance of the TestAppointmentService.
        /// </summary>
        /// <param name="testAppointmentRepository">The test appointment repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public TestAppointmentService(ITestAppointmentRepository testAppointmentRepository)
        {
            _testAppointmentRepository = testAppointmentRepository ?? throw new ArgumentNullException(nameof(testAppointmentRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of test appointments based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for test appointment search</param>
        /// <returns>Response containing test appointment row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<GetAllResponseDataDTO<TestAppointmentRowDTO>>> GetAllAsync(TestAppointmentFilterDTO filterDTO)
        {
            var response = await _testAppointmentRepository.GetAllAsync(filterDTO);
            var data = new GetAllResponseDataDTO<TestAppointmentRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<TestAppointmentRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a paginated list of test appointments for a specific patient.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for test appointment search including patient ID</param>
        /// <returns>Response containing test appointment row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<GetAllResponseDataDTO<TestAppointmentRowDTO>>> GetAllByPatientIDAsync(TestAppointmentFilterDTO filterDTO)
        {
            var response = await _testAppointmentRepository.GetAllByPatientIDAsync(filterDTO);
            var data = new GetAllResponseDataDTO<TestAppointmentRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<TestAppointmentRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a specific test appointment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test appointment</param>
        /// <returns>TestAppointment DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<TestAppointmentDTO>> GetByIDAsync(int id)
        {
            var response = await _testAppointmentRepository.GetByIDAsync(id);
            return ServiceResult<TestAppointmentDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new test appointment in the system.
        /// </summary>
        /// <param name="createTestAppointmentDTO">The test appointment create DTO containing creation information</param>
        /// <returns>The ID of the newly created test appointment</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<int>> AddAsync(CreateTestAppointmentDTO createTestAppointmentDTO)
        {
            var testAppointment = createTestAppointmentDTO.ToTestAppointment();
            var response = await _testAppointmentRepository.AddAsync(testAppointment);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Creates a new test appointment from an existing test order.
        /// </summary>
        /// <param name="createDTO">The DTO containing test order information and appointment details</param>
        /// <returns>The ID of the newly created test appointment</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<int>> AddFromTestOrderAsync(CreateTestAppointmentFromTestOrderDTO createDTO)
        {
            var response = await _testAppointmentRepository.AddFromTestOrderAsync(createDTO);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing test appointment's information.
        /// </summary>
        /// <param name="testAppointment">The test appointment entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> UpdateAsync(TestAppointment testAppointment)
        {
            var response = await _testAppointmentRepository.UpdateAsync(testAppointment);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes a test appointment from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the test appointment to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _testAppointmentRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Starts a test appointment by updating its status to started.
        /// </summary>
        /// <param name="id">The unique identifier of the test appointment to start</param>
        /// <param name="notes">Optional notes for the start operation</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> StartAsync(int id, string? notes)
        {
            var response = await _testAppointmentRepository.StartAsync(id, notes);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Cancels a test appointment by updating its status to cancelled.
        /// </summary>
        /// <param name="id">The unique identifier of the test appointment to cancel</param>
        /// <param name="notes">Optional notes explaining the cancellation reason</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> CancelAsync(int id, string? notes)
        {
            var response = await _testAppointmentRepository.CancelAsync(id, notes);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Completes a test appointment by updating its status to completed.
        /// </summary>
        /// <param name="id">The unique identifier of the test appointment to complete</param>
        /// <param name="notes">Optional notes for the completion</param>
        /// <param name="result">The result or outcome of the test appointment</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> CompleteAsync(int id, string? notes, string result)
        {
            var response = await _testAppointmentRepository.CompleteAsync(id, notes, result);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Reschedules a test appointment to a new date and time.
        /// </summary>
        /// <param name="id">The unique identifier of the test appointment to reschedule</param>
        /// <param name="notes">Optional notes explaining the reschedule reason</param>
        /// <param name="newScheduledDate">The new scheduled date and time</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            var response = await _testAppointmentRepository.RescheduleAsync(id, notes, newScheduledDate);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        public async Task<ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>> GetPaymentsAsync(ServicePaymentsDTO filterDTO)
        {
            var response = await _testAppointmentRepository.GetPaymentsAsync(filterDTO);
            var data = new GetAllResponseDataDTO<PaymentRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>.Create(data, response.Exception); ;
        }
    }
}