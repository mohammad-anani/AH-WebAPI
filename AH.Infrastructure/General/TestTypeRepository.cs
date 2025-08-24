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
    public class TestTypeRepository : ITestTypeRepository
    {
        private readonly ILogger<DepartmentRepository> _logger;

        public TestTypeRepository(ILogger<DepartmentRepository> logger)
        {
            _logger = logger;
        }

        public async Task<ListResponseDTO<TestTypeRowDTO>> GetAllAsync(TestTypeFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Name"] = (filterDTO.Name, SqlDbType.NVarChar, 100, null)
            ,
                ["CostFrom"] = (filterDTO.CostFrom, SqlDbType.NVarChar, 100, null),
                ["CostTo"] = (filterDTO.CostTo, SqlDbType.NVarChar, 100, null)
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

        public async Task<TestType> GetByIDAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
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

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}