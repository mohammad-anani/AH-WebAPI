using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using AH.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AH.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(ILogger<PersonRepository> logger)
        {
            _logger = logger;
        }

        public async Task<bool> EmailAlreadyExists(string email)
        {
            _logger.LogInformation("Checking if email '{Email}' already exists.", email);

            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Email"] = (email, SqlDbType.NVarChar, 40, null),
            };

            SqlParameter existsParam = new SqlParameter
            {
                ParameterName = "@Exists",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };

            try
            {
                Exception? ex = await ADOHelper.ExecuteNonQueryAsync(
                    "EmailAlreadyExists",
                    _logger,
                    cmd =>
                    {
                        _logger.LogDebug("Adding SQL parameters for EmailAlreadyExists SP.");

                        cmd.Parameters.Add(existsParam);
                        SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
                    },
                    null
                );

                if (ex != null)
                {
                    _logger.LogError(ex, "Stored procedure EmailAlreadyExists threw an exception.");
                    return false;
                }

                if (existsParam.Value == null || existsParam.Value == DBNull.Value)
                {
                    _logger.LogWarning("EmailAlreadyExists SP returned a null output parameter.");
                    return false;
                }

                bool exists = (bool)existsParam.Value;

                _logger.LogInformation("EmailAlreadyExists result for '{Email}' = {Exists}", email, exists);

                return exists;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected error while checking if email '{Email}' exists.", email);
                return false;
            }
        }
    }
}