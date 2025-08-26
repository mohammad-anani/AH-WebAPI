using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System;

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
                EmployeeHelper.AddEmployeeParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new AdminRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName"))
            , null);
        }

        public async Task<GetByIDResponseDTO<AdminDTODTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<Admin>("Fetch_AdminByID", _logger, id, null, (reader, converter) =>
            {
                Employee employee = EmployeeHelper.ReadEmployee(reader);
                return new Admin(converter.ConvertValue<int>("ID"), employee);
            });
        }

        public async Task<int> AddAsync(Admin admin)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Admin admin)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> LeaveAsync(int employeeID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}