using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AH.Infrastructure.Repositories
{
    public class TestOrderRepository : ITestOrderRepository
    {
        private readonly ILogger<TestOrderRepository> logger;

        public TestOrderRepository(ILogger<TestOrderRepository> logger)
        {
            this.logger = logger;
        }

        public async Task<GetAllResponseDTO<TestOrderRowDTO>> GetAllAsync(TestOrderFilterDTO filterDTO)
        {
            var extraParameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["AppointmentID"] = (filterDTO.AppointmentID, SqlDbType.Int, null, null),
                ["Page"] = (filterDTO.Page, SqlDbType.Int, null, null),
            };

            int totalCount = -1;
            List<TestOrderRowDTO> items = new List<TestOrderRowDTO>();
            ConvertingHelper converter = new ConvertingHelper();
            RowCountOutputHelper rowCountOutputHelper = new RowCountOutputHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 "Fetch_TestOrders", logger, cmd =>
                 {
                     SqlParameterHelper.AddParametersFromDictionary(cmd, extraParameters);

                     rowCountOutputHelper.AddToCommand(cmd);
                 }, (reader, cmd) =>
                 {
                     items.Add(new TestOrderRowDTO(converter.ConvertValue<int>("ID"),
                         converter.ConvertValue<string>("PatientFullName"),
                         converter.ConvertValue<string>("TestTypeName")));
                 }, null, (reader, cmd) => { converter = new ConvertingHelper(reader); });

            totalCount = rowCountOutputHelper.GetRowCount();

            return new GetAllResponseDTO<TestOrderRowDTO>(items, totalCount, ex);
        }

        public async Task<int> AddAsync(TestOrder testOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetByIDResponseDTO<TestOrder>> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TestOrder testOrder)
        {
            throw new NotImplementedException();
        }
    }
}