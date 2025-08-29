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
        /// <returns>ServiceResult containing prescription row DTOs and count as tuple</returns>
        public async Task<ServiceResult<(IEnumerable<PrescriptionRowDTO> items, int count)>> GetAllByAppointmentIDAsync(PrescriptionFilterDTO filterDTO)
        {
            var response = await _prescriptionRepository.GetAllByAppointmentIDAsync(filterDTO);
            return ServiceResult<(IEnumerable<PrescriptionRowDTO>, int)>.Create((response.Items, response.Count), response.Exception);
        }

        /// <summary>
        /// Retrieves a specific prescription by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription</param>
        /// <returns>ServiceResult containing prescription DTO with complete information or null if not found</returns>
        public async Task<ServiceResult<PrescriptionDTO>> GetByIDAsync(int id)
        {
            var response = await _prescriptionRepository.GetByIDAsync(id);
            return ServiceResult<PrescriptionDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new prescription in the system.
        /// </summary>
        /// <param name="createPrescriptionDTO">The prescription create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created prescription</returns>
        public async Task<ServiceResult<int>> AddAsync(CreatePrescriptionDTO createPrescriptionDTO)
        {
            var prescription = createPrescriptionDTO.ToPrescription();
            var response = await _prescriptionRepository.AddAsync(prescription);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing prescription's information.
        /// </summary>
        /// <param name="updatePrescriptionDTO">The prescription update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> UpdateAsync(UpdatePrescriptionDTO updatePrescriptionDTO)
        {
            var prescription = updatePrescriptionDTO.ToPrescription();
            var response = await _prescriptionRepository.UpdateAsync(prescription);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes a prescription from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _prescriptionRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}