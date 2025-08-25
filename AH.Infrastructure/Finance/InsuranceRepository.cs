using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter.Finance;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;

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

        public async Task<GetByIDResponseDTO<Insurance>> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
            //Insurance insurance = new Insurance();

            //Exception? ex = await ReusableCRUD.GetByID<Admin>("Fetch_InsuranceByID", _logger, id, null, (reader, converter) =>
            //{
            //    insurance  new Insurance(converter.ConvertValue<int>("ID"), new Patient()
            //    {
            //        ID = converter.ConvertValue<int>("PatientID"),
            //        Person = new Person
            //        {
            //            FirstName = converter.ConvertValue<string>("PatientFirstName"),
            //            MiddleName = converter.ConvertValue<string>("PatientMiddleName"),
            //            LastName = converter.ConvertValue<string>("PatientLastName"),
            //        }
            //    },
            //        converter.ConvertValue<string>("ProviderName"),
            //        converter.ConvertValue<decimal>("Coverage", new Department
            //        {
            //            ID = converter.ConvertValue<int>("DepartmentID"),
            //            Name = converter.ConvertValue<string>("DepartmentName")
            //        },
            //        converter.ConvertValue<int>("Cost"),
            //        AdminAuditHelper.ReadAdmin(reader),
            //        converter.ConvertValue<DateTime>("CreatedAt"));
            //}
            //);

            //return new GetByIDResponseDTO<Insurance>(insurance, ex);
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

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}