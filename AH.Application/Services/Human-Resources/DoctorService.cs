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
        /// <returns>ServiceResult containing doctor row DTOs and count as tuple</returns>
        public async Task<ServiceResult<GetAllResponseDataDTO<DoctorRowDTO>>> GetAllAsync(DoctorFilterDTO filterDTO)
        {
            var response = await _doctorRepository.GetAllAsync(filterDTO);
            var data = new GetAllResponseDataDTO<DoctorRowDTO>(response); return ServiceResult<GetAllResponseDataDTO<DoctorRowDTO>>.Create(data, response.Exception);
        }

        /// <summary>
        /// Retrieves a specific doctor by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor</param>
        /// <returns>ServiceResult containing doctor DTO with complete information or null if not found</returns>
        public async Task<ServiceResult<DoctorDTO>> GetByIDAsync(int id)
        {
            var response = await _doctorRepository.GetByIDAsync(id);
            return ServiceResult<DoctorDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new doctor in the system.
        /// </summary>
        /// <param name="createDoctorDTO">The doctor create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created doctor</returns>
        public async Task<ServiceResult<int>> AddAsync(CreateDoctorDTO createDoctorDTO)
        {
            var doctor = createDoctorDTO.ToDoctor();
            var response = await _doctorRepository.AddAsync(doctor);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing doctor's information.
        /// </summary>
        /// <param name="updateDoctorDTO">The doctor update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> UpdateAsync(UpdateDoctorDTO updateDoctorDTO)
        {
            var doctor = updateDoctorDTO.ToDoctor();
            var response = await _doctorRepository.UpdateAsync(doctor);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes a doctor from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _doctorRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Marks a doctor as on leave in the system.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor on leave</param>
        /// <returns>ServiceResult containing true if leave was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> LeaveAsync(int id)
        {
            var response = await _doctorRepository.LeaveAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}