using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Prescription business operations.
    /// Provides a business layer abstraction over prescription repository operations.
    /// </summary>
    public interface IPrescriptionService
    {
        /// <summary>
        /// Retrieves a paginated list of prescriptions for a specific appointment.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for prescription search including appointment ID</param>
        /// <returns>Response containing prescription row DTOs and count</returns>
        Task<GetAllResponseDataDTO<PrescriptionRowDTO>> GetAllByAppointmentIDAsync(PrescriptionFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific prescription by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription</param>
        /// <returns>Prescription DTO with complete information or null if not found</returns>
        Task<PrescriptionDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new prescription in the system.
        /// </summary>
        /// <param name="prescription">The prescription entity to create</param>
        /// <returns>The ID of the newly created prescription</returns>
        Task<int> AddAsync(Prescription prescription);

        /// <summary>
        /// Updates an existing prescription's information.
        /// </summary>
        /// <param name="prescription">The prescription entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(Prescription prescription);

        /// <summary>
        /// Deletes a prescription from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(int id);
    }
}