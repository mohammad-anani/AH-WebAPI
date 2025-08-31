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
    public class OperationRepository : IOperationRepository
    {
        private readonly ILogger<OperationRepository> _logger;

        public OperationRepository(ILogger<OperationRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<OperationRowDTO>> GetAllAsync(OperationFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["DepartmentID"] = (filterDTO.DepartmentID, SqlDbType.Int, null, null),
                ["Name"] = (filterDTO.Name, SqlDbType.NVarChar, 100, null),
                ["Description"] = (filterDTO.Description, SqlDbType.NVarChar, -1, null),
            };

            return await ReusableCRUD.GetAllAsync<OperationRowDTO, OperationFilterDTO>("Fetch_Operations", _logger, filterDTO, cmd =>
            {
                ServiceHelper.AddServiceFilterParameters(filterDTO, cmd);


                var param = cmd.Parameters.AddWithValue("@OperationDoctors", filterDTO.ToOperationDoctorDatatable());
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "dbo.OperationDoctorsType";
            }, (reader, converter) =>

                new OperationRowDTO(converter.ConvertValue<int>("ID"),
                converter.ConvertValue<string>("Name"),
                                    converter.ConvertValue<string>("PatientFullName"),
                                    converter.ConvertValue<DateTime>("ScheduledDate"),
                                    converter.ConvertValue<string>("Status"), converter.ConvertValue<bool>("IsPaid"))
            , parameters);
        }

        public async Task<GetAllResponseDTO<OperationRowDTO>> GetAllByDoctorIDAsync(int doctorID, OperationFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["DoctorID"] = (doctorID, SqlDbType.Int, null, null),
            };

            return await ReusableCRUD.GetAllAsync<OperationRowDTO, OperationFilterDTO>("Fetch_OperationsForDoctor", _logger, filterDTO, cmd =>
            {
                ServiceHelper.AddServiceFilterParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new OperationRowDTO(converter.ConvertValue<int>("ID"),
                converter.ConvertValue<string>("Name"),
                                    converter.ConvertValue<string>("PatientFullName"),
                                    converter.ConvertValue<DateTime>("ScheduledDate"),
                                    converter.ConvertValue<string>("Status"), converter.ConvertValue<bool>("IsPaid"))
            , parameters);
        }

        public async Task<GetAllResponseDTO<OperationRowDTO>> GetAllByPatientIDAsync(OperationFilterDTO filterDTO)
        {
            return await this.GetAllAsync(filterDTO);
        }

        public async Task<GetByIDResponseDTO<OperationDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<OperationDTO>("Fetch_OperationByID", _logger, id, null, (reader, converter) =>
              {
                  ServiceDTO service = ServiceHelper.ReadService(reader);
                  DepartmentRowDTO department = DepartmentRepository.ReadDepartment(reader);
                  AdminRowDTO adminAudit = AdminAuditHelper.ReadAdmin(reader);
                  return new OperationDTO(converter.ConvertValue<int>("ID"), converter.ConvertValue<string>("Name"),
                      department, converter.ConvertValue<string>("Description"), service
             );
              });
        }

        public async Task<CreateResponseDTO> AddAsync(AddUpdateOperationDTO operationDTO)
        {
            Operation operation = operationDTO.Operation;

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Name"] = (operation.Name, SqlDbType.NVarChar, 100, null),
                ["Description"] = (operation.Description, SqlDbType.NVarChar, -1, null),
                ["DepartmentID"] = (operation.Department.ID, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.AddAsync("Create_Operation", _logger, cmd =>
            {
                ServiceHelper.AddCreateServiceParameters(operation.Service, cmd);

                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

                var param = cmd.Parameters.AddWithValue("@OperationDoctors", operationDTO.ToDatatable());
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "dbo.OperationDoctorsType";
            });
        }

        public async Task<SuccessResponseDTO> UpdateAsync(AddUpdateOperationDTO operationDTO)
        {
            Operation operation = operationDTO.Operation;

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Name"] = (operation.Name, SqlDbType.NVarChar, 100, null),
                ["Description"] = (operation.Description, SqlDbType.NVarChar, -1, null),
                ["DepartmentID"] = (operation.Department.ID, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.UpdateAsync("Update_Operation", _logger, operation.ID, cmd =>
            {
                ServiceHelper.AddCreateServiceParameters(operation.Service, cmd);

                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

                var param = cmd.Parameters.AddWithValue("@OperationDoctors", operationDTO.ToDatatable());
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "dbo.OperationDoctorsType";
            });
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_Operation", _logger, id);
        }

        public async Task<SuccessResponseDTO> StartAsync(int id, string? notes)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Start_Operation", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> CancelAsync(int id, string? notes)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Cancel_Operation", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> CompleteAsync(int id, string? notes, string result)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
                ["Result"] = (result, SqlDbType.NVarChar, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Complete_Operation", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
                ["ScheduledDate"] = (newScheduledDate, SqlDbType.DateTime, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Reschedule_Operation", _logger, id, extraParams);
        }

        public async Task<GetAllResponseDTO<PaymentRowDTO>> GetPaymentsAsync(ServicePaymentsDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["OperationID"] = (filterDTO.ID, SqlDbType.Int, null, null),
                ["Page"] = (filterDTO.Page, SqlDbType.Int, null, null),
            };

            int totalCount = -1;
            List<PaymentRowDTO> items = new List<PaymentRowDTO>();
            ConvertingHelper converter = new ConvertingHelper();
            RowCountOutputHelper rowCountOutputHelper = new RowCountOutputHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 "Fetch_OperationPayments", _logger, cmd =>
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