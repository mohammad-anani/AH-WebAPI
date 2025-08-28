using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Create;
using AH.Domain.Entities;

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
        Task<GetAllResponseDataDTO<OperationRowDTO>> GetAllAsync(OperationFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a paginated list of operations for a specific doctor.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        Task<GetAllResponseDataDTO<OperationRowDTO>> GetAllByDoctorIDAsync(int doctorID);

        /// <summary>
        /// Retrieves a paginated list of operations for a specific patient.
        /// </summary>
        /// <param name="patientID">The unique identifier of the patient</param>
        /// <returns>Response containing operation row DTOs and count</returns>
        Task<GetAllResponseDataDTO<OperationRowDTO>> GetAllByPatientIDAsync(int patientID);

        /// <summary>
        /// Retrieves a specific operation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the operation</param>
        /// <returns>Operation DTO with complete information or null if not found</returns>
        Task<OperationDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new operation in the system.
        /// </summary>
        /// <param name="operationDTO">The operation DTO containing creation information</param>
        /// <returns>The ID of the newly created operation</returns>
        Task<int> AddAsync(AddUpdateOperationDTO operationDTO);

        /// <summary>
        /// Updates an existing operation's information.
        /// </summary>
        /// <param name="operationDTO">The operation DTO with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(AddUpdateOperationDTO operationDTO);

        /// <summary>
        /// Deletes an operation from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the operation to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(int id);
    }
}