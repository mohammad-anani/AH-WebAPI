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
    /// Service implementation for Admin business operations.
    /// Acts as a business layer wrapper around the admin repository.
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        /// <summary>
        /// Initializes a new instance of the AdminService.
        /// </summary>
        /// <param name="adminRepository">The admin repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of admins based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for admin search</param>
        /// <returns>Response containing admin row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<GetAllResponseDataDTO<AdminRowDTO>>> GetAllAsync(AdminFilterDTO filterDTO)
        {
            var response = await _adminRepository.GetAllAsync(filterDTO);
            var data = new GetAllResponseDataDTO<AdminRowDTO>(response);
            return ServiceResult<GetAllResponseDataDTO<AdminRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a specific admin by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the admin</param>
        /// <returns>Admin DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<AdminDTO>> GetByIDAsync(int id)
        {
            var response = await _adminRepository.GetByIDAsync(id);
            return ServiceResult<AdminDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new admin in the system.
        /// </summary>
        /// <param name="createAdminDTO">The admin create DTO containing creation information</param>
        /// <returns>The ID of the newly created admin</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<int>> AddAsync(CreateAdminDTO createAdminDTO)
        {
            var admin = createAdminDTO.ToAdmin();
            var response = await _adminRepository.AddAsync(admin);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing admin's information.
        /// </summary>
        /// <param name="admin">The admin entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> UpdateAsync(UpdateAdminDTO admin)
        {
            var response = await _adminRepository.UpdateAsync(admin.ToAdmin());
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes an admin from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the admin to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _adminRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Marks an admin as having left the organization by setting their leave date.
        /// This is a non-destructive operation that maintains the admin record for audit purposes.
        /// </summary>
        /// <param name="id">The unique identifier of the admin who is leaving</param>
        /// <returns>True if leave processing was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> LeaveAsync(int id)
        {
            var response = await _adminRepository.LeaveAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}