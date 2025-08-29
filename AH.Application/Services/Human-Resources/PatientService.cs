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
    /// Service implementation for Patient business operations.
    /// Acts as a business layer wrapper around the patient repository.
    /// </summary>
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        /// <summary>
        /// Initializes a new instance of the PatientService.
        /// </summary>
        /// <param name="patientRepository">The patient repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of patients based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for patient search</param>
        /// <returns>ServiceResult containing patient row DTOs and count as tuple</returns>
        public async Task<ServiceResult<(IEnumerable<PatientRowDTO> items, int count)>> GetAllAsync(PatientFilterDTO filterDTO)
        {
            var response = await _patientRepository.GetAllAsync(filterDTO);
            return ServiceResult<(IEnumerable<PatientRowDTO>, int)>.Create((response.Items, response.Count), response.Exception);
        }

        /// <summary>
        /// Retrieves a paginated list of patients for a specific doctor based on filter criteria.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <param name="filterDTO">Filter criteria for patient search</param>
        /// <returns>ServiceResult containing patient row DTOs and count as tuple</returns>
        public async Task<ServiceResult<(IEnumerable<PatientRowDTO> items, int count)>> GetAllForDoctorAsync(int doctorID, PatientFilterDTO filterDTO)
        {
            var response = await _patientRepository.GetAllForDoctorAsync(doctorID, filterDTO);
            return ServiceResult<(IEnumerable<PatientRowDTO>, int)>.Create((response.Items, response.Count), response.Exception);
        }

        /// <summary>
        /// Retrieves a specific patient by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the patient</param>
        /// <returns>ServiceResult containing patient DTO with complete information or null if not found</returns>
        public async Task<ServiceResult<PatientDTO>> GetByIDAsync(int id)
        {
            var response = await _patientRepository.GetByIDAsync(id);
            return ServiceResult<PatientDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new patient in the system.
        /// </summary>
        /// <param name="createPatientDTO">The patient create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created patient</returns>
        public async Task<ServiceResult<int>> AddAsync(CreatePatientDTO createPatientDTO)
        {
            var patient = createPatientDTO.ToPatient();
            var response = await _patientRepository.AddAsync(patient);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing patient's information.
        /// </summary>
        /// <param name="updatePatientDTO">The patient update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> UpdateAsync(UpdatePatientDTO updatePatientDTO)
        {
            var patient = updatePatientDTO.ToPatient();
            var response = await _patientRepository.UpdateAsync(patient);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes a patient from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the patient to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _patientRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}