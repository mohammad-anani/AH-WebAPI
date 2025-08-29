using AH.Application.DTOs.Create;
using AH.Application.DTOs.Update;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

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
        /// <returns>ServiceResult containing insurance row DTOs and count as tuple</returns>
        Task<ServiceResult<(IEnumerable<InsuranceRowDTO> items, int count)>> GetAllByPatientIDAsync(InsuranceFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific insurance record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the insurance record</param>
        /// <returns>ServiceResult containing insurance DTO with complete information or null if not found</returns>
        Task<ServiceResult<InsuranceDTO?>> GetByIDAsync(int id);

        /// <summary>
        /// Renews an existing insurance policy with updated coverage and expiration date.
        /// </summary>
        /// <param name="ID">The unique identifier of the insurance record to renew</param>
        /// <param name="coverage">The new coverage amount</param>
        /// <param name="expirationDate">The new expiration date</param>
        /// <returns>ServiceResult containing true if renewal was successful, false otherwise</returns>
        Task<ServiceResult<bool>> RenewAsync(int ID, decimal coverage, DateOnly expirationDate);

        /// <summary>
        /// Creates a new insurance record in the system.
        /// </summary>
        /// <param name="createInsuranceDTO">The insurance create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created insurance record</returns>
        Task<ServiceResult<int>> AddAsync(CreateInsuranceDTO createInsuranceDTO);

        /// <summary>
        /// Updates an existing insurance record's information.
        /// </summary>
        /// <param name="updateInsuranceDTO">The insurance update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdateInsuranceDTO updateInsuranceDTO);

        /// <summary>
        /// Deletes an insurance record from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the insurance record to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}