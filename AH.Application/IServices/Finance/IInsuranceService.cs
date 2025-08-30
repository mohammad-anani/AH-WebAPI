using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;
using System.Threading;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Insurance business operations.
    /// Provides a business layer abstraction over insurance repository operations.
    /// </summary>
    public interface IInsuranceService
    {
        /// <summary>
        /// Retrieves a paginated list of insurance records for a specific patient based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for insurance search including patient ID</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing insurance row DTOs and count as tuple</returns>
        Task<ServiceResult<GetAllResponseDataDTO<InsuranceRowDTO>>> GetAllByPatientIDAsync(InsuranceFilterDTO filterDTO, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a specific insurance record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the insurance record</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing insurance DTO with complete information or null if not found</returns>
        Task<ServiceResult<InsuranceDTO>> GetByIDAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Renews an existing insurance policy with updated coverage and expiration date.
        /// </summary>
        /// <param name="ID">The unique identifier of the insurance record to renew</param>
        /// <param name="coverage">The new coverage amount</param>
        /// <param name="expirationDate">The new expiration date</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing true if renewal was successful, false otherwise</returns>
        Task<ServiceResult<bool>> RenewAsync(RenewInsuranceDTO renewInsuranceDTO, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new insurance record in the system.
        /// </summary>
        /// <param name="createInsuranceDTO">The insurance create DTO containing creation information</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing the ID of the newly created insurance record</returns>
        Task<ServiceResult<int>> AddAsync(CreateInsuranceDTO createInsuranceDTO, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing insurance record's information.
        /// </summary>
        /// <param name="updateInsuranceDTO">The insurance update DTO with updated information</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdateInsuranceDTO updateInsuranceDTO, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an insurance record from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the insurance record to delete</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}