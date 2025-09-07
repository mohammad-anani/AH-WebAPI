using AH.Application.DTOs.Create;
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
    public class TestAppointmentRepository : ITestAppointmentRepository
    {
        private readonly ILogger<TestAppointmentRepository> _logger;

        public TestAppointmentRepository(ILogger<TestAppointmentRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<TestAppointmentRowDTO>> GetAllAsync(TestAppointmentFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["TestOrderID"] = (filterDTO.TestOrderID, SqlDbType.Int, null, null),
                ["TestTypeID"] = (filterDTO.TestTypeID, SqlDbType.Int, null, null),
            };

            return await ReusableCRUD.GetAllAsync<TestAppointmentRowDTO, TestAppointmentFilterDTO>("Fetch_TestAppointments", _logger, filterDTO, cmd =>
            {
                ServiceHelper.AddServiceFilterParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new TestAppointmentRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("PatientFullName"),
                                    converter.ConvertValue<string>("TestTypeName"),
                                    converter.ConvertValue<bool>("IsOrdered"),
                                    converter.ConvertValue<DateTime>("ScheduledDate"),
                                    converter.ConvertValue<string>("Status"),
                                    converter.ConvertValue<bool>("IsPaid")
                                    )
            , parameters);
        }

        public async Task<GetAllResponseDTO<TestAppointmentRowDTO>> GetAllByPatientIDAsync(TestAppointmentFilterDTO filterDTO)
        {
            return await GetAllAsync(filterDTO);
        }

        public async Task<GetByIDResponseDTO<TestAppointmentDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<TestAppointmentDTO>("Fetch_TestAppointmentByID", _logger, id, null, (reader, converter) =>
            {
                int? testorderid = converter.ConvertValue<int?>("TestOrderID");

                TestTypeRowDTO testType = TestTypeRepository.ReadTestType(reader);
                return new TestAppointmentDTO(converter.ConvertValue<int>("ID"), testorderid != null ? new TestOrderRowDTO(testorderid ?? -1,
converter.ConvertValue<string>("TestOrderPatientFullName"),
                    converter.ConvertValue<string>("TestOrderTestTypeName")) : null, testType,
ServiceHelper.ReadService(reader));
            }
            );
        }

        public async Task<CreateResponseDTO> AddAsync(TestAppointment testAppointment)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["TestTypeID"] = (testAppointment.TestType.ID, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.AddAsync("Create_TestAppointment", _logger, cmd =>
            {
                ServiceHelper.AddCreateServiceParameters(testAppointment.Service, cmd);

                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            });
        }

        public async Task<CreateResponseDTO> AddFromTestOrderAsync(CreateTestAppointmentFromTestOrderDTO app)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["TestOrderID"] = (app.TestOrderID, SqlDbType.Int, null, null),
                ["ScheduledDate"] = (app.ScheduledDate, SqlDbType.DateTime, null, null),
                ["Notes"] = (app.Notes, SqlDbType.NVarChar, -1, null),
                ["CreatedByReceptionistID"] = (app.CreatedByReceptionistID, SqlDbType.Int, null, null),
                ["Status"] = (3, SqlDbType.TinyInt, null, null)
            };

            return await ReusableCRUD.AddAsync("Create_TestAppointmentFromTestOrder", _logger, cmd =>
            {
                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            });
        }

        public async Task<SuccessResponseDTO> UpdateAsync(TestAppointment testAppointment)
        {
            return await ReusableCRUD.UpdateAsync("Update_TestAppointment", _logger, testAppointment.ID, cmd =>
            {
                ServiceHelper.AddUpdateServiceParameters(testAppointment.Service, cmd);
            });
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_TestAppointment", _logger, id);
        }

        public async Task<SuccessResponseDTO> StartAsync(int id, string? notes)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, 500, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Start_TestAppointment", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> CancelAsync(int id, string? notes)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, 500, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Cancel_TestAppointment", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> CompleteAsync(int id, string? notes, string result)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, 500, null),
                ["Result"] = (result, SqlDbType.NVarChar, 500, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Complete_TestAppointment", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, 500, null),
                ["ScheduledDate"] = (newScheduledDate, SqlDbType.DateTime, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Reschedule_TestAppointment", _logger, id, extraParams);
        }

        public async Task<GetAllResponseDTO<PaymentRowDTO>> GetPaymentsAsync(ServicePaymentsDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["TestAppointmentID"] = (filterDTO.ID, SqlDbType.Int, null, null),
                ["Page"] = (filterDTO.Page, SqlDbType.Int, null, null),
            };

            int totalCount = -1;
            List<PaymentRowDTO> items = new List<PaymentRowDTO>();
            ConvertingHelper converter = new ConvertingHelper();
            RowCountOutputHelper rowCountOutputHelper = new RowCountOutputHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 "Fetch_TestAppointmentPayments", _logger, cmd =>
                 {
                     SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

                     rowCountOutputHelper.AddToCommand(cmd);
                 }, (reader, cmd) =>
                 {
                     items.Add(new PaymentRowDTO(
                         converter.ConvertValue<int>("ID"),
                         converter.ConvertValue<int>("Amount"),
                         converter.ConvertValue<string>("Method")
                        ));
                 }, null, (reader, cmd) => { converter = new ConvertingHelper(reader); });

            totalCount = rowCountOutputHelper.GetRowCount();

            return new GetAllResponseDTO<PaymentRowDTO>(items, totalCount, ex);
        }
    }
}