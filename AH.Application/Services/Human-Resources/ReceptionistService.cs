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
    /// Service implementation for Receptionist business operations.
    /// Acts as a business layer wrapper around the receptionist repository.
    /// </summary>
    public class ReceptionistService : IReceptionistService
    {
        private readonly IReceptionistRepository _receptionistRepository;

        /// <summary>
        /// Initializes a new instance of the ReceptionistService.
        /// </summary>
        /// <param name="receptionistRepository">The receptionist repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public ReceptionistService(IReceptionistRepository receptionistRepository)
        {
            _receptionistRepository = receptionistRepository ?? throw new ArgumentNullException(nameof(receptionistRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of receptionists based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for receptionist search</param>
        /// <returns>Response containing receptionist row DTOs and count</returns>
        public async Task<ServiceResult<GetAllResponseDataDTO<ReceptionistRowDTO>>> GetAllAsync(ReceptionistFilterDTO filterDTO)
        {
            var response = await _receptionistRepository.GetAllAsync(filterDTO);
            var data = new GetAllResponseDataDTO<ReceptionistRowDTO>(response);return ServiceResult<GetAllResponseDataDTO<ReceptionistRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a specific receptionist by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the receptionist</param>
        /// <returns>Receptionist DTO with complete information or null if not found</returns>
        public async Task<ServiceResult<ReceptionistDTO>> GetByIDAsync(int id)
        {
            var response = await _receptionistRepository.GetByIDAsync(id);
            return ServiceResult<ReceptionistDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new receptionist in the system.
        /// </summary>
        /// <param name="createReceptionistDTO">The receptionist create DTO containing creation information</param>
        /// <returns>The ID of the newly created receptionist</returns>
        public async Task<ServiceResult<int>> AddAsync(CreateReceptionistDTO createReceptionistDTO)
        {
            var receptionist = createReceptionistDTO.ToReceptionist();
            var response = await _receptionistRepository.AddAsync(receptionist);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing receptionist's information.
        /// </summary>
        /// <param name="receptionist">The receptionist entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> UpdateAsync(Receptionist receptionist)
        {
            var response = await _receptionistRepository.UpdateAsync(receptionist);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes a receptionist from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the receptionist to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _receptionistRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Marks a receptionist as having left the organization by setting their leave date.
        /// This is a non-destructive operation that maintains the receptionist record for audit purposes.
        /// </summary>
        /// <param name="id">The unique identifier of the receptionist who is leaving</param>
        /// <returns>True if leave processing was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> LeaveAsync(int id)
        {
            var response = await _receptionistRepository.LeaveAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}