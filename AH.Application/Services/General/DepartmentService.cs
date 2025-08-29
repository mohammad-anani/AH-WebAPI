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
        /// <returns>ServiceResult containing department row DTOs and count as tuple</returns>
        public async Task<ServiceResult<(IEnumerable<DepartmentRowDTO> items, int count)>> GetAllAsync(DepartmentFilterDTO filterDTO)
        {
            var response = await _departmentRepository.GetAllAsync(filterDTO);
            return ServiceResult<(IEnumerable<DepartmentRowDTO>, int)>.Create((response.Items, response.Count), response.Exception);
        }

        /// <summary>
        /// Retrieves a specific department by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the department</param>
        /// <returns>ServiceResult containing department DTO with complete information or null if not found</returns>
        public async Task<ServiceResult<DepartmentDTO>> GetByIDAsync(int id)
        {
            var response = await _departmentRepository.GetByIDAsync(id);
            return ServiceResult<DepartmentDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new department in the system.
        /// </summary>
        /// <param name="createDepartmentDTO">The department create DTO containing creation information</param>
        /// <returns>ServiceResult containing the ID of the newly created department</returns>
        public async Task<ServiceResult<int>> AddAsync(CreateDepartmentDTO createDepartmentDTO)
        {
            var department = createDepartmentDTO.ToDepartment();
            var response = await _departmentRepository.AddAsync(department);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Updates an existing department's information.
        /// </summary>
        /// <param name="updateDepartmentDTO">The department update DTO with updated information</param>
        /// <returns>ServiceResult containing true if update was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> UpdateAsync(UpdateDepartmentDTO updateDepartmentDTO)
        {
            var department = updateDepartmentDTO.ToDepartment();
            var response = await _departmentRepository.UpdateAsync(department);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }

        /// <summary>
        /// Deletes a department from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the department to delete</param>
        /// <returns>ServiceResult containing true if deletion was successful, false otherwise</returns>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _departmentRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}