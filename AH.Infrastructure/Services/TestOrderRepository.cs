using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
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

        public async Task<CreateResponseDTO> AddAsync(TestOrder testOrder)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["TestTypeID"] = (testOrder.TestType.ID, SqlDbType.Int, null, null),
                ["AppointmentID"] = (testOrder.Appointment.ID, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.AddAsync("Create_TestOrder", logger, cmd =>
            {
                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            });
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_TestOrder", logger, id);
        }

        public async Task<GetByIDResponseDTO<TestOrderDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<TestOrderDTO>("Fetch_TestOrderByID", logger, id, null, (reader, converter) =>
            {
                TestTypeRowDTO testType = TestTypeRepository.ReadTestType(reader);
                AppointmentRowDTO appointment = AppointmentRepository.ReadAppointment(reader, "")??new AppointmentRowDTO();
                return new TestOrderDTO(converter.ConvertValue<int>("ID"), appointment, testType
                      );
            });
        }
    }
}