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
        public async Task<GetAllResponseDataDTO<AdminRowDTO>> GetAllAsync(AdminFilterDTO filterDTO)
        {
            var response = await _adminRepository.GetAllAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve admins.", response.Exception);
            }

            return new GetAllResponseDataDTO<AdminRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a specific admin by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the admin</param>
        /// <returns>Admin DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<AdminDTO?> GetByIDAsync(int id)
        {
            var response = await _adminRepository.GetByIDAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve admin with ID {id}.", response.Exception);
            }

            return response.Item;
        }

        /// <summary>
        /// Creates a new admin in the system.
        /// </summary>
        /// <param name="createAdminDTO">The admin create DTO containing creation information</param>
        /// <returns>The ID of the newly created admin</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddAsync(CreateAdminDTO createAdminDTO)
        {
            var admin = createAdminDTO.ToAdmin();
            var response = await _adminRepository.AddAsync(admin);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create admin.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Updates an existing admin's information.
        /// </summary>
        /// <param name="admin">The admin entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> UpdateAsync(Admin admin)
        {
            var response = await _adminRepository.UpdateAsync(admin);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to update admin with ID {admin.ID}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Deletes an admin from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the admin to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _adminRepository.DeleteAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to delete admin with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Marks an admin as having left the organization by setting their leave date.
        /// This is a non-destructive operation that maintains the admin record for audit purposes.
        /// </summary>
        /// <param name="id">The unique identifier of the admin who is leaving</param>
        /// <returns>True if leave processing was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> LeaveAsync(int id)
        {
            var response = await _adminRepository.LeaveAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to process leave for admin with ID {id}.", response.Exception);
            }

            return response.Success;
        }
    }
}