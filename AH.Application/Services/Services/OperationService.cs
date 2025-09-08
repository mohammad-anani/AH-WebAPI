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
    /// Service implementation for Operation business operations.
    /// Acts as a business layer wrapper around the operation repository.
    /// </summary>
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _operationRepository;

        /// <summary>
        /// Initializes a new instance of the OperationService.
        /// </summary>
        /// <param name="operationRepository">The operation repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public OperationService(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository ?? throw new ArgumentNullException(nameof(operationRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of operations based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for operation search</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<GetAllResponseDataDTO<OperationRowDTO>>> GetAllAsync(OperationFilterDTO filterDTO)
        {
            var response = await _operationRepository.GetAllAsync(filterDTO);
            var data = new GetAllResponseDataDTO<OperationRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<OperationRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a paginated list of operations for a specific doctor.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<GetAllResponseDataDTO<OperationRowDTO>>> GetAllByDoctorIDAsync(int doctorID, OperationFilterDTO filterDTO)
        {
            var response = await _operationRepository.GetAllByDoctorIDAsync(doctorID, filterDTO);
            var data = new GetAllResponseDataDTO<OperationRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<OperationRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a paginated list of operations for a specific patient.
        /// </summary>
        /// <param name="patientID">The unique identifier of the patient</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<GetAllResponseDataDTO<OperationRowDTO>>> GetAllByPatientIDAsync(OperationFilterDTO filterDTO)
        {
            var response = await _operationRepository.GetAllByPatientIDAsync(filterDTO);
            var data = new GetAllResponseDataDTO<OperationRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<OperationRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a specific operation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the operation</param>
        /// <returns>Operation DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<OperationDTO>> GetByIDAsync(int id)
        {
            var response = await _operationRepository.GetByIDAsync(id);
            return ServiceResult<OperationDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new operation in the system.
        /// </summary>
        /// <param name="createOperationDTO">The operation create DTO containing creation information</param>
        /// <returns>The ID of the newly created operation</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<int>> AddAsync(CreateOperationDTO createOperationDTO)
        {
            var addUpdateOperationDTO = createOperationDTO.ToAddUpdateOperationDTO();
            var response = await _operationRepository.AddAsync(addUpdateOperationDTO);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing operation's information.
        /// </summary>
        /// <param name="operationDTO">The operation DTO with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> UpdateAsync(UpdateOperationDTO operationDTO)
        {
            var response = await _operationRepository.UpdateAsync(operationDTO.ToAddUpdateOperationDTO());
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes an operation from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _operationRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Starts an operation by updating its status to started.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to start</param>
        /// <param name="notes">Optional notes for the start operation</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> StartAsync(int id, string? notes)
        {
            var response = await _operationRepository.StartAsync(id, notes);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Cancels an operation by updating its status to cancelled.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to cancel</param>
        /// <param name="notes">Optional notes explaining the cancellation reason</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> CancelAsync(int id, string? notes)
        {
            var response = await _operationRepository.CancelAsync(id, notes);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Completes an operation by updating its status to completed.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to complete</param>
        /// <param name="notes">Optional notes for the completion</param>
        /// <param name="result">The result or outcome of the operation</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> CompleteAsync(int id, string? notes, string result)
        {
            var response = await _operationRepository.CompleteAsync(id, notes, result);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Reschedules an operation to a new date and time.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to reschedule</param>
        /// <param name="notes">Optional notes explaining the reschedule reason</param>
        /// <param name="newScheduledDate">The new scheduled date and time</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            var response = await _operationRepository.RescheduleAsync(id, notes, newScheduledDate);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        public async Task<ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>> GetPaymentsAsync(ServicePaymentsDTO filterDTO)
        {
            var response = await _operationRepository.GetPaymentsAsync(filterDTO);
            var data = new GetAllResponseDataDTO<PaymentRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>.Create(data, response.Exception); ;
        }

        public async Task<ServiceResult<int>> PayAsync(int id, CreateServicePaymentDTO dto)
        {
            var response = await _operationRepository.PayAsync(id, dto.Amount, dto.Method, dto.CreatedByReceptionistID);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }
    }
}