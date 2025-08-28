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
    /// Service implementation for Doctor business operations.
    /// Acts as a business layer wrapper around the doctor repository.
    /// </summary>
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        /// <summary>
        /// Initializes a new instance of the DoctorService.
        /// </summary>
        /// <param name="doctorRepository">The doctor repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of doctors based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for doctor search</param>
        /// <returns>Response containing doctor row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<DoctorRowDTO>> GetAllAsync(DoctorFilterDTO filterDTO)
        {
            var response = await _doctorRepository.GetAllAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve doctors.", response.Exception);
            }

            return new GetAllResponseDataDTO<DoctorRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a specific doctor by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor</param>
        /// <returns>Doctor DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<DoctorDTO?> GetByIDAsync(int id)
        {
            var response = await _doctorRepository.GetByIDAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve doctor with ID {id}.", response.Exception);
            }

            return response.Item;
        }

        /// <summary>
        /// Creates a new doctor in the system.
        /// </summary>
        /// <param name="doctor">The doctor entity to create</param>
        /// <returns>The ID of the newly created doctor</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddAsync(Doctor doctor)
        {
            var response = await _doctorRepository.AddAsync(doctor);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create doctor.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Updates an existing doctor's information.
        /// </summary>
        /// <param name="doctor">The doctor entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            var response = await _doctorRepository.UpdateAsync(doctor);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to update doctor with ID {doctor.ID}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Deletes a doctor from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _doctorRepository.DeleteAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to delete doctor with ID {id}.", response.Exception);
            }

            return response.Success;
        }

        public async Task<bool> LeaveAsync(int id)
        {
            var response = await _doctorRepository.LeaveAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to leave doctor with ID {id}.", response.Exception);
            }

            return response.Success;
        }
    }
}