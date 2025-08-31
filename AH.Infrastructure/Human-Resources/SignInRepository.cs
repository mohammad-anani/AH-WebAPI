using AH.Application.DTOs.Response;
using AH.Application.IRepositories;
using AH.Infrastructure.Helpers;
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
    public class SignInRepository : ISigninRepository
    {
        private readonly ILogger<SignInRepository> logger;

        public SignInRepository(ILogger<SignInRepository> logger)
        {
            this.logger = logger;
        }

        public async Task<SigninResponseDTO> SigninAsync(string email, string password)
        {
            var IDParam = new SqlParameter();
            IDParam.ParameterName = "@ID";
            IDParam.SqlDbType = SqlDbType.Int;
            IDParam.Direction = ParameterDirection.Output;

            var RoleParam = new SqlParameter();
            RoleParam.ParameterName = "@Role";
            RoleParam.SqlDbType = SqlDbType.NVarChar;
            RoleParam.Size = 20;

            var @params = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "@Email", (email, SqlDbType.Int, null, null)
                },
                { "@Password", (password, SqlDbType.NVarChar, 256, null)
                }
            };

            Exception? ex = await ADOHelper.ExecuteNonQueryAsync(
                 "FindEmployeeByEmailAndPassword", logger, cmd =>
                 {
                     SqlParameterHelper.AddParametersFromDictionary(cmd, @params);
                     cmd.Parameters.Add(IDParam);
                     cmd.Parameters.Add(RoleParam);
                 }, null);

            if (ex != null && (ex.Message.Contains("does not exist") || ex.Message.Contains("not found") || ex.Message.Contains("Invalid") && ex.Message.Contains("ID")))
            {
                return new SigninResponseDTO(-1, string.Empty, new Exception("Invalid email or password."));
            }

            if (IDParam.Value == DBNull.Value || IDParam.Value == null || RoleParam.Value == DBNull.Value || RoleParam.Value == null)
                return new SigninResponseDTO(-1, string.Empty, new InvalidDataException());

            return new SigninResponseDTO((int)IDParam.Value, (string)RoleParam.Value, ex);
        }
    }
}