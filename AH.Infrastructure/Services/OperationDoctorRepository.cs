using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;
using AH.Application.DTOs.Create;

namespace AH.Infrastructure.Repositories
{
    public class OperationDoctorRepository : IOperationDoctorRepository
    {
        private readonly ILogger<OperationDoctorRepository> _logger;

        public OperationDoctorRepository(ILogger<OperationDoctorRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<OperationDoctorRowDTO>> GetAllByOperationIDAsync(OperationDoctorFilterDTO filterDTO)
        {
            var extraParameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["OperationID"] = (filterDTO.OperationID, SqlDbType.Int, null, null),
                ["Page"] = (filterDTO.Page, SqlDbType.Int, null, null),
            };

            int totalCount = -1;
            List<OperationDoctorRowDTO> items = new List<OperationDoctorRowDTO>();
            ConvertingHelper converter = new ConvertingHelper();
            RowCountOutputHelper rowCountOutputHelper = new RowCountOutputHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 "Fetch_OperationDoctors", _logger, cmd =>
                 {
                     SqlParameterHelper.AddParametersFromDictionary(cmd, extraParameters);

                     rowCountOutputHelper.AddToCommand(cmd);
                 }, (reader, cmd) =>
                 {
                     items.Add(new OperationDoctorRowDTO(
                         converter.ConvertValue<int>("ID"),
                         converter.ConvertValue<int>("DoctorID"),
                         converter.ConvertValue<string>("DoctorFullName"),
                         converter.ConvertValue<string>("Role")
                         ));
                 }, null, (reader, cmd) => { converter = new ConvertingHelper(reader); });

            totalCount = rowCountOutputHelper.GetRowCount();

            return new GetAllResponseDTO<OperationDoctorRowDTO>(items, totalCount, ex);
        }

        public async Task<SuccessResponseDTO> AddUpdateAsync(AddUpdateOperationDoctorDTO opDoctDTO)
        {
            SuccessOutputHelper successOutputHelper = new SuccessOutputHelper();

            Exception? ex = await ADOHelper.ExecuteNonQueryAsync("AddUpdate_OperationDoctor", _logger, cmd =>
         {
             successOutputHelper.AddToCommand(cmd);
             var param = cmd.Parameters.AddWithValue("@OperationDoctors", opDoctDTO.ToDatatable());
             param.SqlDbType = SqlDbType.Structured;
             param.TypeName = "dbo.OperationDoctorsType";
             cmd.Parameters.AddWithValue("@OperationID", opDoctDTO.OperationID);
         }, null);

            return new SuccessResponseDTO(successOutputHelper.GetResult(), ex);
        }
    }
}