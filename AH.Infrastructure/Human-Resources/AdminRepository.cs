using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace AH.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing Admin entities with CRUD operations and employee leave functionality.
    /// Administrators are employees with elevated privileges in the hospital system.
    /// </summary>
    public class AdminRepository : IAdminRepository
    {
        private readonly ILogger<AdminRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the AdminRepository class.
        /// </summary>
        /// <param name="logger">The logger instance for structured logging</param>
        /// <exception cref="ArgumentNullException">Thrown when logger is null</exception>
        public AdminRepository(ILogger<AdminRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves a paginated list of admins based on the provided filter criteria.
        /// Filters are applied through employee-level filtering (department, salary, hire date, etc.).
        /// </summary>
        /// <param name="filterDTO">Filter criteria including pagination, sorting, and employee filters</param>
        /// <returns>A response containing the list of admin rows, total count, and any exception that occurred</returns>
        /// <remarks>
        /// Uses the Fetch_Admins stored procedure. Returns AdminRowDTO objects containing ID and FullName.
        /// Employee-level filters include department, salary range, hire date range, shift times, and audit information.
        /// </remarks>
        public async Task<GetAllResponseDTO<AdminRowDTO>> GetAllAsync(AdminFilterDTO filterDTO)
        {
            _logger.LogInformation("Retrieving admins with filters - Page: {Page}, Sort: {Sort}, Order: {Order}",
                filterDTO.Page, filterDTO.Sort, filterDTO.Order);

            var result = await ReusableCRUD.GetAllAsync<AdminRowDTO, AdminFilterDTO>("Fetch_Admins", _logger, filterDTO, cmd =>
            {
                EmployeeHelper.AddEmployeeFilterParameters(filterDTO, cmd);
            }, (reader, converter) =>
                new AdminRowDTO(converter.ConvertValue<int>("ID"),
                                converter.ConvertValue<string>("FullName"))
            , null);

            if (result.Exception == null)
            {
                _logger.LogInformation("Successfully retrieved {Count} admins", result.Count);
            }
            else
            {
                _logger.LogError(result.Exception, "Failed to retrieve admins");
            }

            return result;
        }

        /// <summary>
        /// Retrieves a specific admin by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the admin</param>
        /// <returns>A response containing the admin details or an exception if the operation failed</returns>
        /// <remarks>
        /// Uses the Fetch_AdminByID stored procedure. Returns an AdminDTO containing the admin ID and complete employee information.
        /// The employee information includes personal details, department, salary, and audit data.
        /// </remarks>
        public async Task<GetByIDResponseDTO<AdminDTO>> GetByIDAsync(int id)
        {
            _logger.LogInformation("Retrieving admin with ID: {AdminId}", id);

            var result = await ReusableCRUD.GetByID<AdminDTO>("Fetch_AdminByID", _logger, id, null, (reader, converter) =>
            {
                EmployeeDTO employee = EmployeeHelper.ReadEmployee(reader);
                return new AdminDTO(converter.ConvertValue<int>("ID"), employee);
            });

            if (result.Exception == null)
            {
                _logger.LogInformation("Successfully retrieved admin with ID: {AdminId}", id);
            }
            else
            {
                _logger.LogError(result.Exception, "Failed to retrieve admin with ID: {AdminId}", id);
            }

            return result;
        }

        /// <summary>
        /// Creates a new admin in the system.
        /// </summary>
        /// <param name="admin">The admin entity containing employee information and admin-specific data</param>
        /// <returns>A response containing the new admin ID or an exception if the operation failed</returns>
        /// <remarks>
        /// Uses the Create_Admin stored procedure. Creates both the employee record and admin privileges.
        /// Employee parameters include personal information, department assignment, salary, shift times, and working days.
        /// The operation is handled within stored procedure business logic for data consistency.
        /// </remarks>
        public async Task<CreateResponseDTO> AddAsync(Admin admin)
        {
            _logger.LogInformation("Creating new admin for employee in department: {DepartmentId}",
                admin.Employee.Department?.ID);

            var result = await ReusableCRUD.AddAsync("Create_Admin", _logger, (cmd) =>
            {
                EmployeeHelper.AddCreateEmployeeParameters(admin.Employee, cmd);
            });

            if (result.Exception == null)
            {
                _logger.LogInformation("Successfully created admin with ID: {AdminId}", result.ID);
            }
            else
            {
                _logger.LogError(result.Exception, "Failed to create admin");
            }

            return result;
        }

        /// <summary>
        /// Updates an existing admin's information.
        /// </summary>
        /// <param name="admin">The admin entity with updated information</param>
        /// <returns>A response indicating success/failure and any exception that occurred</returns>
        /// <remarks>
        /// Uses the Update_Admin stored procedure. Updates employee-level information such as personal details,
        /// department assignment, salary, shift times, and working days. Admin-specific privileges remain unchanged.
        /// The admin ID is used to identify the record to update.
        /// </remarks>
        public async Task<SuccessResponseDTO> UpdateAsync(Admin admin)
        {
            _logger.LogInformation("Updating admin with ID: {AdminId}", admin.ID);

            var result = await ReusableCRUD.UpdateAsync("Update_Admin", _logger, admin.ID, (cmd) =>
            {
                EmployeeHelper.AddUpdateEmployeeParameters(admin.Employee, cmd);
            });

            if (result.Exception == null)
            {
                _logger.LogInformation("Successfully updated admin with ID: {AdminId}, Success: {Success}",
                    admin.ID, result.Success);
            }
            else
            {
                _logger.LogError(result.Exception, "Failed to update admin with ID: {AdminId}", admin.ID);
            }

            return result;
        }

        /// <summary>
        /// Permanently deletes an admin from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the admin to delete</param>
        /// <returns>A response indicating success/failure and any exception that occurred</returns>
        /// <remarks>
        /// Uses the Delete_Admin stored procedure. This is a hard delete operation that removes both
        /// admin privileges and the underlying employee record. Consider using LeaveAsync for non-destructive operations.
        /// Future role-based security implementation will prevent admins from deleting themselves.
        /// </remarks>
        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            _logger.LogWarning("Attempting to delete admin with ID: {AdminId}", id);

            var result = await ReusableCRUD.DeleteAsync("Delete_Admin", _logger, id);

            if (result.Exception == null)
            {
                _logger.LogWarning("Admin deletion completed - ID: {AdminId}, Success: {Success}",
                    id, result.Success);
            }
            else
            {
                _logger.LogError(result.Exception, "Failed to delete admin with ID: {AdminId}", id);
            }

            return result;
        }

        /// <summary>
        /// Marks an admin as having left the organization by setting their leave date.
        /// This is a non-destructive operation that maintains the admin record for audit purposes.
        /// </summary>
        /// <param name="ID">The unique identifier of the admin who is leaving</param>
        /// <returns>A response indicating success/failure and any exception that occurred</returns>
        /// <remarks>
        /// Uses the Leave_Admin stored procedure. Sets the employee's leave date to the current date,
        /// effectively marking them as inactive while preserving their record for historical and audit purposes.
        /// The admin retains their ID and historical data but is no longer considered active in the system.
        /// This is the recommended approach for admin departures rather than deletion.
        /// </remarks>
        public async Task<SuccessResponseDTO> LeaveAsync(int ID)
        {
            _logger.LogInformation("Processing leave request for admin with ID: {AdminId}", ID);

            var result = await ReusableCRUD.ExecuteByIDAsync("Leave_Admin", _logger, ID, null);

            if (result.Exception == null)
            {
                _logger.LogInformation("Admin leave processed - ID: {AdminId}, Success: {Success}",
                    ID, result.Success);
            }
            else
            {
                _logger.LogError(result.Exception, "Failed to process leave for admin with ID: {AdminId}", ID);
            }

            return result;
        }
    }
}