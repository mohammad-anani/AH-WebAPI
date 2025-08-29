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
        /// <returns>Response containing patient row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<PatientRowDTO>> GetAllAsync(PatientFilterDTO filterDTO)
        {
            var response = await _patientRepository.GetAllAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve patients.", response.Exception);
            }

            return new GetAllResponseDataDTO<PatientRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a paginated list of patients for a specific doctor based on filter criteria.
        /// </summary>
        /// <param name="doctorID">The unique identifier of the doctor</param>
        /// <param name="filterDTO">Filter criteria for patient search</param>
        /// <returns>Response containing patient row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<PatientRowDTO>> GetAllForDoctorAsync(int doctorID, PatientFilterDTO filterDTO)
        {
            var response = await _patientRepository.GetAllForDoctorAsync(doctorID, filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve patients for doctor with ID {doctorID}.", response.Exception);
            }

            return new GetAllResponseDataDTO<PatientRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a specific patient by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the patient</param>
        /// <returns>Patient DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<PatientDTO?> GetByIDAsync(int id)
        {
            var response = await _patientRepository.GetByIDAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve patient with ID {id}.", response.Exception);
            }

            return response.Item;
        }

        /// <summary>
        /// Creates a new patient in the system.
        /// </summary>
        /// <param name="createPatientDTO">The patient create DTO containing creation information</param>
        /// <returns>The ID of the newly created patient</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddAsync(CreatePatientDTO createPatientDTO)
        {
            var patient = createPatientDTO.ToPatient();
            var response = await _patientRepository.AddAsync(patient);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create patient.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Updates an existing patient's information.
        /// </summary>
        /// <param name="patient">The patient entity with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> UpdateAsync(Patient patient)
        {
            var response = await _patientRepository.UpdateAsync(patient);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to update patient with ID {patient.ID}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Deletes a patient from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the patient to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _patientRepository.DeleteAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to delete patient with ID {id}.", response.Exception);
            }

            return response.Success;
        }
    }
}