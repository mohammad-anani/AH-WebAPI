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
                                    converter.ConvertValue<string>("TestName"),
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
                TestTypeRowDTO testType = TestTypeRepository.ReadTestType(reader);
                return new TestAppointmentDTO(converter.ConvertValue<int>("ID"), new TestOrderRowDTO(
                    converter.ConvertValue<int>("TestOrderID"), converter.ConvertValue<string>("TestOrderPatientFullName"),
                    converter.ConvertValue<string>("TestOrderTestTypeName")), testType,
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
                ServiceHelper.AddCreateServiceParameters(testAppointment.Service, cmd);
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
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Start_TestAppointment", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> CancelAsync(int id, string? notes)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Cancel_TestAppointment", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> CompleteAsync(int id, string? notes, string result)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
                ["Result"] = (result, SqlDbType.NVarChar, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Complete_TestAppointment", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
                ["ScheduledDate"] = (newScheduledDate, SqlDbType.DateTime, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Reschedule_TestAppointment", _logger, id, extraParams);
        }
    }
}