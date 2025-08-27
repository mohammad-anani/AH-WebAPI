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
    public class AdminRepository : IAdminRepository
    {
        private readonly ILogger<AdminRepository> _logger;

        public AdminRepository(ILogger<AdminRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<AdminRowDTO>> GetAllAsync(AdminFilterDTO filterDTO)
        {
            return await ReusableCRUD.GetAllAsync<AdminRowDTO, AdminFilterDTO>("Fetch_Admins", _logger, filterDTO, cmd =>
            {
                EmployeeHelper.AddEmployeeFilterParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new AdminRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName"))
            , null);
        }

        public async Task<GetByIDResponseDTO<AdminDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<AdminDTO>("Fetch_AdminByID", _logger, id, null, (reader, converter) =>
            {
                EmployeeDTO employee = EmployeeHelper.ReadEmployee(reader);
                return new AdminDTO(converter.ConvertValue<int>("ID"), employee);
            });
        }

        public async Task<CreateResponseDTO> AddAsync(Admin admin)
        {
            return await ReusableCRUD.AddAsync("Create_Admin", _logger, (cmd) =>
            {
                EmployeeHelper.AddCreateEmployeeParameters(admin.Employee, cmd);
            });
        }

        public async Task<SuccessResponseDTO> UpdateAsync(Admin admin)
        {
            return await ReusableCRUD.UpdateAsync("Update_Admin", _logger, admin.ID, (cmd) =>
            {
                EmployeeHelper.AddUpdateEmployeeParameters(admin.Employee, cmd);
            });
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_Admin", _logger, id);
        }

        public async Task<SuccessResponseDTO> LeaveAsync(int ID)
        {
            return await ReusableCRUD.ExecuteByIDAsync("Leave_Admin", _logger, ID, null);
        }
    }
}