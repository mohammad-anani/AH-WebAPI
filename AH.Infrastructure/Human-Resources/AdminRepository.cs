using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System;
using AH.Application.DTOs.Entities;

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

        public async Task<GetByIDResponseDTO<AdminDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<AdminDTO>("Fetch_AdminByID", _logger, id, null, (reader, converter) =>
            {
                EmployeeDTO employee = EmployeeHelper.ReadEmployee(reader);
                return new AdminDTO(converter.ConvertValue<int>("ID"), employee);
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