using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;
using AH.Application.IRepositories;
using AH.Application.IServices;
using System.Threading;

namespace AH.Application.Services
{
    /// <summary>
    /// Service implementation for Insurance business operations.
    /// Acts as a business layer wrapper around the insurance repository.
    /// </summary>
    public class InsuranceService : IInsuranceService
    {
        private readonly IInsuranceRepository _insuranceRepository;

        /// <summary>
        /// Initializes a new instance of the InsuranceService.
        /// </summary>
        /// <param name="insuranceRepository">The insurance repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public InsuranceService(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository ?? throw new ArgumentNullException(nameof(insuranceRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of insurance records for a specific patient based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for insurance search including patient ID</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing insurance row DTOs and count as tuple</returns>
        public async Task<ServiceResult<(IEnumerable<InsuranceRowDTO> items, int count)>> GetAllByPatientIDAsync(InsuranceFilterDTO filterDTO, CancellationToken cancellationToken = default)
        {
            var response = await _insuranceRepository.GetAllByPatientIDAsync(filterDTO, cancellationToken);
            return ServiceResult<(IEnumerable<InsuranceRowDTO>, int)>.Create((response.Items, response.Count), response.Exception);
        }

        /// <summary>
        /// Retrieves a specific insurance record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the insurance record</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing insurance DTO with complete information or null if not found</returns>
        public async Task<ServiceResult<InsuranceDTO>> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = await _insuranceRepository.GetByIDAsync(id, cancellationToken);
            return ServiceResult<InsuranceDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Renews an existing insurance policy with updated coverage and expiration date.
        /// </summary>
        /// <param name="ID">The unique identifier of the insurance record to renew</param>
        /// <param name="coverage">The new coverage amount</param>
        /// <param name="expirationDate">The new expiration date</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing true if renewal was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> RenewAsync(RenewInsuranceDTO renewInsuranceDTO, CancellationToken cancellationToken = default)
        {
            var response = await _insuranceRepository.Renew(renewInsuranceDTO.ID, renewInsuranceDTO.Coverage, renewInsuranceDTO.ExpirationDate, cancellationToken);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Creates a new insurance record in the system.
        /// </summary>
        /// <param name="createInsuranceDTO">The insurance create DTO containing creation information</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing the ID of the newly created insurance record</returns>
        public async Task<ServiceResult<int>> AddAsync(CreateInsuranceDTO createInsuranceDTO, CancellationToken cancellationToken = default)
        {
            var insurance = createInsuranceDTO.ToInsurance();
            var response = await _insuranceRepository.AddAsync(insurance, cancellationToken);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing insurance record's information.
        /// </summary>
        /// <param name="updateInsuranceDTO">The insurance update DTO with updated information</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> UpdateAsync(UpdateInsuranceDTO updateInsuranceDTO, CancellationToken cancellationToken = default)
        {
            var insurance = updateInsuranceDTO.ToInsurance();
            var response = await _insuranceRepository.UpdateAsync(insurance, cancellationToken);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes an insurance record from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the insurance record to delete</param>
        /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = await _insuranceRepository.DeleteAsync(id, cancellationToken);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}