using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AH.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(ILogger<DepartmentRepository> logger)
        {
            _logger = logger;
        }

        public async Task<ListResponseDTO<DepartmentRowDTO>> GetAllAsync(DepartmentFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Name"] = (filterDTO.Name, SqlDbType.NVarChar, 100, null)
                ,
                ["Phone"] = (filterDTO.Phone, SqlDbType.NVarChar, 100, null)
            };

            return await ReusableCRUD.GetAllAsync<DepartmentRowDTO, DepartmentFilterDTO>("Fetch_Departments", _logger, filterDTO, cmd =>
            {
                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
                AdminAuditHelper.AddAdminAuditParameters(filterDTO.CreatedByAdminID, filterDTO.CreatedAtFrom, filterDTO.CreatedAtTo, cmd);
            }, (reader, converter) =>

                new DepartmentRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("Name"), converter.ConvertValue<string>("Phone"))
     , null);
        }

        public async Task<Department> GetByIDAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Department department)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}