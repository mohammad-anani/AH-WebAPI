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
    /// Service implementation for Department business operations.
    /// Acts as a business layer wrapper around the department repository.
    /// </summary>
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        /// <summary>
        /// Initializes a new instance of the DepartmentService.
        /// </summary>
        /// <param name="departmentRepository">The department repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of departments based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for department search</param>
        /// <returns>Enumerable of department row DTOs</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<DepartmentRowDTO>> GetAllAsync(DepartmentFilterDTO filterDTO)
        {
            var response = await _departmentRepository.GetAllAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve departments.", response.Exception);
            }

            return new GetAllResponseDataDTO<DepartmentRowDTO>(response);
        }

        /// <summary>
        /// Retrieves a specific department by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the department</param>
        /// <returns>Department DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<DepartmentDTO?> GetByIDAsync(int id)
        {
            var response = await _departmentRepository.GetByIDAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to retrieve department with ID {id}.", response.Exception);
            }

            return response.Item;
        }

        /// <summary>
        /// Creates a new department in the system.
        /// </summary>
        /// <param name="createDepartmentDTO">The department create DTO containing creation information</param>
        /// <returns>The ID of the newly created department</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<int> AddAsync(CreateDepartmentDTO createDepartmentDTO)
        {
            var department = createDepartmentDTO.ToDepartment();
            var response = await _departmentRepository.AddAsync(department);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to create department.", response.Exception);
            }

            return response.ID;
        }

        /// <summary>
        /// Updates an existing department's information.
        /// </summary>
        /// <param name="updateDepartmentDTO">The department update DTO with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> UpdateAsync(UpdateDepartmentDTO updateDepartmentDTO)
        {
            var department = updateDepartmentDTO.ToDepartment();
            var response = await _departmentRepository.UpdateAsync(department);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to update department with ID {updateDepartmentDTO.ID}.", response.Exception);
            }

            return response.Success;
        }

        /// <summary>
        /// Deletes a department from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the department to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _departmentRepository.DeleteAsync(id);

            if (response.Exception != null)
            {
                throw new InvalidOperationException($"Failed to delete department with ID {id}.", response.Exception);
            }

            return response.Success;
        }
    }
}