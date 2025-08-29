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
        public async Task<GetAllResponseDataDTO<OperationRowDTO>> GetAllAsync(OperationFilterDTO filterDTO)
        {
            var response = await _operationRepository.GetAllAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve operations.", response.Exception);
            }

            return new GetAllResponseDataDTO<OperationRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a paginated list of operations for a specific doctor.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<OperationRowDTO>> GetAllByDoctorIDAsync(int doctorID)
        {
            var response = await _operationRepository.GetAllByDoctorIDAsync(doctorID);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve operations by doctor ID.", response.Exception);
            }

            return new GetAllResponseDataDTO<OperationRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a paginated list of operations for a specific patient.
        /// </summary>
        /// <param name="patientID">The unique identifier of the patient</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<OperationRowDTO>> GetAllByPatientIDAsync(int patientID)
        {
            var response = await _operationRepository.GetAllByPatientIDAsync(patientID);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve operations by patient ID.", response.Exception);
            }

            return new GetAllResponseDataDTO<OperationRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a specific operation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the operation</param>
        /// <returns>Operation DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<OperationDTO?> GetByIDAsync(int id)
        {
            var response = await _operationRepository.GetByIDAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve operation with ID {id}.", response.Exception);
            }

            return response.Item;
        }

        /// <summary>
        /// Creates a new operation in the system.
        /// </summary>
        /// <param name="createOperationDTO">The operation create DTO containing creation information</param>
        /// <returns>The ID of the newly created operation</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddAsync(CreateOperationDTO createOperationDTO)
        {
            var addUpdateOperationDTO = createOperationDTO.ToAddUpdateOperationDTO();
            var response = await _operationRepository.AddAsync(addUpdateOperationDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create operation.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Updates an existing operation's information.
        /// </summary>
        /// <param name="operationDTO">The operation DTO with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> UpdateAsync(AddUpdateOperationDTO operationDTO)
        {
            var response = await _operationRepository.UpdateAsync(operationDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to update operation.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Deletes an operation from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _operationRepository.DeleteAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to delete operation with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Starts an operation by updating its status to started.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to start</param>
        /// <param name="notes">Optional notes for the start operation</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> StartAsync(int id, string? notes)
        {
            var response = await _operationRepository.StartAsync(id, notes);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to start operation with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Cancels an operation by updating its status to cancelled.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to cancel</param>
        /// <param name="notes">Optional notes explaining the cancellation reason</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> CancelAsync(int id, string? notes)
        {
            var response = await _operationRepository.CancelAsync(id, notes);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to cancel operation with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Completes an operation by updating its status to completed.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to complete</param>
        /// <param name="notes">Optional notes for the completion</param>
        /// <param name="result">The result or outcome of the operation</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> CompleteAsync(int id, string? notes, string result)
        {
            var response = await _operationRepository.CompleteAsync(id, notes, result);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to complete operation with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Reschedules an operation to a new date and time.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to reschedule</param>
        /// <param name="notes">Optional notes explaining the reschedule reason</param>
        /// <param name="newScheduledDate">The new scheduled date and time</param>
        /// <returns>True if the operation was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            var response = await _operationRepository.RescheduleAsync(id, notes, newScheduledDate);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to reschedule operation with ID {id}.", response.Exception);
            }

            return response.Success;
        }
    }
}