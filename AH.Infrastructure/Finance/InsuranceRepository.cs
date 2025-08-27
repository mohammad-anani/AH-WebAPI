using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter.Finance;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;
using AH.Application.DTOs.Entities;

namespace AH.Infrastructure.Repositories
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly ILogger<InsuranceRepository> _logger;

        public InsuranceRepository(ILogger<InsuranceRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<InsuranceRowDTO>> GetAllByPatientIDAsync(InsuranceFilterDTO filterDTO)
        {
            var extraParameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["PatientID"] = (filterDTO.PatientID, SqlDbType.Int, null, null),
                ["Page"] = (filterDTO.Page, SqlDbType.Int, null, null),
            };

            int totalCount = -1;
            List<InsuranceRowDTO> items = new List<InsuranceRowDTO>();
            ConvertingHelper converter = new ConvertingHelper();
            RowCountOutputHelper rowCountOutputHelper = new RowCountOutputHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 "Fetch_Insurances", _logger, cmd =>
                 {
                     SqlParameterHelper.AddParametersFromDictionary(cmd, extraParameters);

                     rowCountOutputHelper.AddToCommand(cmd);
                 }, (reader, cmd) =>
                 {
                     items.Add(new InsuranceRowDTO(
                         converter.ConvertValue<int>("ID"),
                         converter.ConvertValue<string>("ProviderName"),
                         converter.ConvertValue<decimal>("Coverage"),
                         converter.ConvertValue<bool>("IsActive")));
                 }, null, (reader, cmd) => { converter = new ConvertingHelper(reader); });

            totalCount = rowCountOutputHelper.GetRowCount();

            return new GetAllResponseDTO<InsuranceRowDTO>(items, totalCount, ex);
        }

        public async Task<GetByIDResponseDTO<InsuranceDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<InsuranceDTO>("Fetch_InsuranceByID", _logger, id, null, (reader, converter) =>

                new InsuranceDTO(
                    converter.ConvertValue<int>("ID"),
                    PatientRepository.ReadPatient(reader),
                    converter.ConvertValue<string>("ProviderName"),
                    converter.ConvertValue<decimal>("Coverage"),
                    converter.ConvertValue<DateTime>("ExpirationDate"),
                    converter.ConvertValue<bool>("IsActive"),
                       converter.ConvertValue<DateTime>("CreatedAt"), ReceptionistAuditHelper.ReadReceptionist(reader)));
        }

        public async Task<bool> Renew(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Insurance insurance)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Insurance insurance)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_Insurance", _logger, id);
        }
    }
}