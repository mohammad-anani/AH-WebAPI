using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using Microsoft.Data.SqlClient;

using AH.Infrastructure.Helpers;
using System.Data;
using Microsoft.Extensions.Logging;

namespace AH.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private ILogger<AdminRepository> _Logger { get; set; }

        public AdminRepository(ILogger<AdminRepository> logger)
        {
            _Logger = logger;
        }

        public async Task<(IEnumerable<AdminRowDTO> Items, int Count)> GetAllAsync(AdminFilterDTO filterDTO)
        {
            int totalCount = -1;
            List<AdminRowDTO> admins = new List<AdminRowDTO>();
            SqlParameter output = new SqlParameter
            {
                ParameterName = "@TotalRowCount",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 "Fetch_Admins", cmd =>
                 {
                     EmployeeHelper.AddEmployeeParameters(filterDTO)(cmd);

                     var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
                     {
                         ["Page"] = (filterDTO.Page, SqlDbType.Int, null, null),
                         ["Sort"] = (filterDTO.Sort, SqlDbType.NVarChar, 20, null),
                         ["Order"] = (filterDTO.Sort, SqlDbType.Bit, null, null),

                     };

                     SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

                     cmd.Parameters.Add(output);

                 }, (reader, cmd) =>
                 {
                     var converter = new ConvertingHelper(reader);
                     admins.Add(new AdminRowDTO(converter.ConvertValue<int>("ID"),
                        converter.ConvertValue<string>("FullName")

                         ));

                 }, (cmd) =>
                 {
                     totalCount = (int)output.Value;
                 });

            return (admins, totalCount);
        }

        public async Task<Admin> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Admin admin)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Admin admin)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> LeaveAsync(int employeeID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}