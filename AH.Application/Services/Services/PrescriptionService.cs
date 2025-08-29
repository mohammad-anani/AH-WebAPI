using AH.Application.DTOs.Create;
using AH.Application.DTOs.Update;
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
    /// Service implementation for Prescription business operations.
    /// Acts as a business layer wrapper around the prescription repository.
    /// </summary>
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        /// <summary>
        /// Initializes a new instance of the PrescriptionService.
        /// </summary>
        /// <param name="prescriptionRepository">The prescription repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository ?? throw new ArgumentNullException(nameof(prescriptionRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of prescriptions for a specific appointment.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for prescription search including appointment ID</param>
        /// <returns>Response containing prescription row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<PrescriptionRowDTO>> GetAllByAppointmentIDAsync(PrescriptionFilterDTO filterDTO)
        {
            var response = await _prescriptionRepository.GetAllByAppointmentIDAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve prescriptions by appointment ID.", response.Exception);
            }

            return new GetAllResponseDataDTO<PrescriptionRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a specific prescription by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription</param>
        /// <returns>Prescription DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<PrescriptionDTO?> GetByIDAsync(int id)
        {
            var response = await _prescriptionRepository.GetByIDAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve prescription with ID {id}.", response.Exception);
            }

            return response.Item;
        }

        /// <summary>
        /// Creates a new prescription in the system.
        /// </summary>
        /// <param name="createPrescriptionDTO">The prescription create DTO containing creation information</param>
        /// <returns>The ID of the newly created prescription</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddAsync(CreatePrescriptionDTO createPrescriptionDTO)
        {
            var prescription = createPrescriptionDTO.ToPrescription();
            var response = await _prescriptionRepository.AddAsync(prescription);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create prescription.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Updates an existing prescription's information.
        /// </summary>
        /// <param name="updatePrescriptionDTO">The prescription update DTO with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> UpdateAsync(UpdatePrescriptionDTO updatePrescriptionDTO)
        {
            var prescription = updatePrescriptionDTO.ToPrescription();
            var response = await _prescriptionRepository.UpdateAsync(prescription);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to update prescription with ID {updatePrescriptionDTO.ID}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Deletes a prescription from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _prescriptionRepository.DeleteAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to delete prescription with ID {id}.", response.Exception);
            }

            return response.Success;
        }
    }
}