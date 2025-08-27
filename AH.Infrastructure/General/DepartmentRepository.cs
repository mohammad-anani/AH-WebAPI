using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
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

        public async Task<GetAllResponseDTO<DepartmentRowDTO>> GetAllAsync(DepartmentFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Name"] = (filterDTO.Name, SqlDbType.NVarChar, 20, null)
                ,
                ["Phone"] = (filterDTO.Phone, SqlDbType.NVarChar, 8, null)
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

        public async Task<GetByIDResponseDTO<DepartmentDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<DepartmentDTO>("Fetch_DepartmentByID", _logger, id, null, (reader, converter) =>
            new DepartmentDTO(converter.ConvertValue<int>("ID"), converter.ConvertValue<string>("Name"),
                    converter.ConvertValue<string>("Phone"),
                    AdminAuditHelper.ReadAdmin(reader),
                    converter.ConvertValue<DateTime>("CreatedAt"))

            );
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

        public static DepartmentRowDTO ReadDepartment(SqlDataReader reader)
        {
            ConvertingHelper converter = new ConvertingHelper(reader);

            return new DepartmentRowDTO(converter.ConvertValue<int>("ID"), converter.ConvertValue<string>("DepartmentName"),
               converter.ConvertValue<string>("DepartmentPhone")
            );
        }
    }
}