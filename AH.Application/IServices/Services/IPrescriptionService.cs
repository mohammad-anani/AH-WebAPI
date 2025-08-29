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
    /// Service interface for Prescription business operations.
    /// Provides a business layer abstraction over prescription repository operations.
    /// </summary>
    public interface IPrescriptionService
    {
        /// <summary>
        /// Retrieves a paginated list of prescriptions for a specific appointment.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for prescription search including appointment ID</param>
        /// <returns>ServiceResult containing prescription row DTOs and count as tuple</returns>
        Task<ServiceResult<(IEnumerable<PrescriptionRowDTO> items, int count)>> GetAllByAppointmentIDAsync(PrescriptionFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific prescription by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription</param>
        /// <returns>ServiceResult containing prescription DTO with complete information or null if not found</returns>
        Task<ServiceResult<PrescriptionDTO?>> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new prescription in the system.
        /// </summary>
        /// <param name="createPrescriptionDTO">The prescription create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created prescription</returns>
        Task<ServiceResult<int>> AddAsync(CreatePrescriptionDTO createPrescriptionDTO);

        /// <summary>
        /// Updates an existing prescription's information.
        /// </summary>
        /// <param name="updatePrescriptionDTO">The prescription update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        Task<ServiceResult<bool>> UpdateAsync(UpdatePrescriptionDTO updatePrescriptionDTO);

        /// <summary>
        /// Deletes a prescription from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}