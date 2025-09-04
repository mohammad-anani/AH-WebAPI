using AH.Application.DTOs.Create;
using AH.Application.DTOs.Response;
using AH.Application.IRepositories;
using AH.Domain.Entities;
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
    public class AuthRepository : ISigninRepository
    {
        private readonly ILogger<AuthRepository> logger;

        public AuthRepository(ILogger<AuthRepository> logger)
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
                { "@Email", (email, SqlDbType.NVarChar, 40, null)
                },
                { "@Password", (CreatePersonDTO.HashPassword(password), SqlDbType.NVarChar, 256, null)
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

        public async Task<(string? token, DateTime? ExpiryDate)> GetRefreshTokenByUserAsync(int id, string role)
        {
            var TokenParam = new SqlParameter();
            TokenParam.ParameterName = "@RefreshToken";
            TokenParam.SqlDbType = SqlDbType.NVarChar;
            TokenParam.Size = -1;
            TokenParam.Direction = ParameterDirection.Output;

            var ExpiryDateParam = new SqlParameter();
            ExpiryDateParam.ParameterName = "@ExpiryDate";
            ExpiryDateParam.SqlDbType = SqlDbType.DateTime;

            var @params = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "@ID", (id, SqlDbType.Int, null, null)
                },
                { "@Role", (role, SqlDbType.NVarChar, -1, null)
                }
            };

            Exception? ex = await ADOHelper.ExecuteNonQueryAsync(
                 "FindEmployeeByEmailAndPassword", logger, cmd =>
                 {
                     SqlParameterHelper.AddParametersFromDictionary(cmd, @params);
                     cmd.Parameters.Add(TokenParam);
                     cmd.Parameters.Add(ExpiryDateParam);
                 }, null);

            if (ex != null)
            {
                throw new Exception();
            }

            if (TokenParam.Value == DBNull.Value || TokenParam.Value == null || ExpiryDateParam.Value == DBNull.Value || ExpiryDateParam.Value == null)
                return (null, null);

            return (TokenParam.Value.ToString(), (DateTime)ExpiryDateParam.Value);
        }

        public async Task<bool> UpdateUserRefreshTokenAsync(int userId, string role, string refreshToken, DateTime expiryDate)
        {
            SuccessOutputHelper successOutputHelper = new SuccessOutputHelper();
            // Input parameters dictionary
            var @params = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
    {
        { "@UserID", (userId, SqlDbType.Int, null, null) },
        { "@Role", (role, SqlDbType.NVarChar, -1, null) },
        { "@RefreshToken", (refreshToken, SqlDbType.NVarChar, -1, null) },
        { "@ExpiryDate", (expiryDate, SqlDbType.DateTime, null, null) }
    };

            // Execute the stored procedure
            Exception? ex = await ADOHelper.ExecuteNonQueryAsync(
                "dbo.UpdateUserRefreshToken",
                logger,
                cmd =>
                {
                    SqlParameterHelper.AddParametersFromDictionary(cmd, @params);
                    successOutputHelper.AddToCommand(cmd); // Add the output parameter
                },
                null
            );

            if (ex != null)
            {
                throw new Exception("Failed to execute UpdateUserRefreshToken SP", ex);
            }

            // Return the success bit as boolean
            bool successParam = successOutputHelper.GetResult();
            return successParam;
        }

        public async Task<bool> UpdateUserRefreshTokenAsync(string email, string refreshToken, DateTime expiryDate)
        {
            SuccessOutputHelper successOutputHelper = new SuccessOutputHelper();
            // Input parameters dictionary
            var @params = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
    {
        { "@Email", (email, SqlDbType.NVarChar, 40, null) },
        { "@RefreshToken", (refreshToken, SqlDbType.NVarChar, -1, null) },
        { "@ExpiryDate", (expiryDate, SqlDbType.DateTime, null, null) }
    };

            // Execute the stored procedure
            Exception? ex = await ADOHelper.ExecuteNonQueryAsync(
                "dbo.UpdateUserRefreshToken",
                logger,
                cmd =>
                {
                    SqlParameterHelper.AddParametersFromDictionary(cmd, @params);
                    successOutputHelper.AddToCommand(cmd); // Add the output parameter
                },
                null
            );

            if (ex != null)
            {
                throw new Exception("Failed to execute UpdateUserRefreshToken SP", ex);
            }

            // Return the success bit as boolean
            bool successParam = successOutputHelper.GetResult();
            return successParam;
        }
    }
}