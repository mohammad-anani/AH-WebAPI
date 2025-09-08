using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Operation business operations.
    /// Provides a business layer abstraction over operation repository operations.
    /// </summary>
    public interface IOperationService : IService
    {
        /// <summary>
        /// Retrieves a paginated list of operations based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for operation search</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        Task<ServiceResult<GetAllResponseDataDTO<OperationRowDTO>>> GetAllAsync(OperationFilterDTO filterDTO);

        Task<ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>> GetPaymentsAsync(ServicePaymentsDTO filterDTO);

        /// <summary>
        /// Retrieves a paginated list of operations for a specific doctor.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        Task<ServiceResult<GetAllResponseDataDTO<OperationRowDTO>>> GetAllByDoctorIDAsync(int doctorID, OperationFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a paginated list of operations for a specific patient.
        /// </summary>
        /// <param name="patientID">The unique identifier of the patient</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        Task<ServiceResult<GetAllResponseDataDTO<OperationRowDTO>>> GetAllByPatientIDAsync(OperationFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific operation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the operation</param>
        /// <returns>Operation DTO with complete information or null if not found</returns>
        Task<ServiceResult<OperationDTO>> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new operation in the system.
        /// </summary>
        /// <param name="createOperationDTO">The operation create DTO containing creation information</param>
        /// <returns>The ID of the newly created operation</returns>
        Task<ServiceResult<int>> AddAsync(CreateOperationDTO createOperationDTO);

        /// <summary>
        /// Updates an existing operation's information.
        /// </summary>
        /// <param name="operationDTO">The operation DTO with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdateOperationDTO operationDTO);

        /// <summary>
        /// Deletes an operation from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);

        /// <summary>
        /// Starts the operation with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to start</param>
        /// <param name="notes">Optional notes regarding the operation start</param>
        /// <returns>True if the operation was successfully started, false otherwise</returns>
        Task<ServiceResult<bool>> StartAsync(int id, string? notes);

        /// <summary>
        /// Cancels the operation with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to cancel</param>
        /// <param name="notes">Optional notes regarding the operation cancellation</param>
        /// <returns>True if the operation was successfully canceled, false otherwise</returns>
        Task<ServiceResult<bool>> CancelAsync(int id, string? notes);

        /// <summary>
        /// Completes the operation with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to complete</param>
        /// <param name="notes">Optional notes regarding the operation completion</param>
        /// <param name="result">The result of the operation</param>
        /// <returns>True if the operation was successfully completed, false otherwise</returns>
        Task<ServiceResult<bool>> CompleteAsync(int id, string? notes, string result);

        /// <summary>
        /// Reschedules the operation with the specified ID to a new date.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to reschedule</param>
        /// <param name="notes">Optional notes regarding the rescheduling</param>
        /// <param name="newScheduledDate">The new date and time for the operation</param>
        /// <returns>True if the operation was successfully rescheduled, false otherwise</returns>
        Task<ServiceResult<bool>> RescheduleAsync(int id, string? notes, DateTime newScheduledDate);

        /// <summary>
        /// Pays for the operation with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to pay for</param>
        /// <param name="dto">The service payment DTO containing payment information</param>
        /// <returns>The ID of the created payment record</returns>
        Task<ServiceResult<int>> PayAsync(int id, CreateServicePaymentDTO dto);
    }
}