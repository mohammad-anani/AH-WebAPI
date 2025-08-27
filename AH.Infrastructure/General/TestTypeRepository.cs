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
    public class TestTypeRepository : ITestTypeRepository
    {
        private readonly ILogger<TestTypeRepository> _logger;

        public TestTypeRepository(ILogger<TestTypeRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<TestTypeRowDTO>> GetAllAsync(TestTypeFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Name"] = (filterDTO.Name, SqlDbType.NVarChar, 100, null),
                ["DepartmentID"] = (filterDTO.DepartmentID, SqlDbType.Int, null, null),
                ["CostFrom"] = (filterDTO.CostFrom, SqlDbType.Int, null, null),
                ["CostTo"] = (filterDTO.CostTo, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.GetAllAsync<TestTypeRowDTO, TestTypeFilterDTO>("Fetch_TestTypes", _logger, filterDTO, cmd =>
            {
                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
                AdminAuditHelper.AddAdminAuditParameters(filterDTO.CreatedByAdminID, filterDTO.CreatedAtFrom, filterDTO.CreatedAtTo, cmd);
            }, (reader, converter) =>

                new TestTypeRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("Name"), converter.ConvertValue<string>("DepartmentName"), converter.ConvertValue<int>("Cost"))
     , null);
        }

        public async Task<GetByIDResponseDTO<TestTypeDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<TestTypeDTO>("Fetch_TestTypeByID", _logger, id, null, (reader, converter) =>
            {
                return new TestTypeDTO(converter.ConvertValue<int>("ID"), converter.ConvertValue<string>("Name"), new DepartmentRowDTO(
                    converter.ConvertValue<int>("DepartmentID"),
                    converter.ConvertValue<string>("DepartmentName"),
                    converter.ConvertValue<string>("DepartmentPhone")
                ),
                    converter.ConvertValue<int>("Cost"),
                    AdminAuditHelper.ReadAdmin(reader),
                    converter.ConvertValue<DateTime>("CreatedAt"));
            });
        }

        public async Task<int> AddAsync(TestType testType)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TestType testType)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_TestType", _logger, id);
        }

        public static TestTypeRowDTO ReadTestType(SqlDataReader reader)
        {
            ConvertingHelper converter = new ConvertingHelper(reader);

            return new TestTypeRowDTO(
                converter.ConvertValue<int>("TestTypeID"),
                converter.ConvertValue<string>("TestTypeName"),
                converter.ConvertValue<string>("TestTypeDepartmentName"),
                converter.ConvertValue<int>("TestTypeCost")
            );
        }
    }
}