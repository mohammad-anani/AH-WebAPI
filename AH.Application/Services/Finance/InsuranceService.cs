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
        /// <returns>Response containing insurance row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<InsuranceRowDTO>> GetAllByPatientIDAsync(InsuranceFilterDTO filterDTO)
        {
            var response = await _insuranceRepository.GetAllByPatientIDAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve insurance records by patient ID.", response.Exception);
            }

            return new GetAllResponseDataDTO<InsuranceRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a specific insurance record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the insurance record</param>
        /// <returns>Insurance DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<InsuranceDTO?> GetByIDAsync(int id)
        {
            var response = await _insuranceRepository.GetByIDAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve insurance record with ID {id}.", response.Exception);
            }

            return response.Item;
        }

        /// <summary>
        /// Renews an existing insurance policy with updated coverage and expiration date.
        /// </summary>
        /// <param name="ID">The unique identifier of the insurance record to renew</param>
        /// <param name="coverage">The new coverage amount</param>
        /// <param name="expirationDate">The new expiration date</param>
        /// <returns>True if renewal was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> RenewAsync(int ID, decimal coverage, DateOnly expirationDate)
        {
            var response = await _insuranceRepository.Renew(ID, coverage, expirationDate);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to renew insurance record with ID {ID}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Creates a new insurance record in the system.
        /// </summary>
        /// <param name="createInsuranceDTO">The insurance create DTO containing creation information</param>
        /// <returns>The ID of the newly created insurance record</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddAsync(CreateInsuranceDTO createInsuranceDTO)
        {
            var insurance = createInsuranceDTO.ToInsurance();
            var response = await _insuranceRepository.AddAsync(insurance);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create insurance record.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Updates an existing insurance record's information.
        /// </summary>
        /// <param name="insurance">The insurance entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> UpdateAsync(Insurance insurance)
        {
            var response = await _insuranceRepository.UpdateAsync(insurance);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to update insurance record with ID {insurance.ID}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Deletes an insurance record from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the insurance record to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _insuranceRepository.DeleteAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to delete insurance record with ID {id}.", response.Exception);
            }

            return response.Success;
        }
    }
}