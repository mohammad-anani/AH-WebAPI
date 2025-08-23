using AH.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AH.Infrastructure.Helpers
{
    public static class ADOHelper
    {
        private static readonly string _connectionString = ConfigHelper.GetConnectionString();

        /// <summary>
        /// Execute a stored procedure that returns rows (via SqlDataReader).
        /// </summary>
        public static async Task<Exception?> ExecuteReaderAsync(
            string spName,
            Action<SqlCommand>? addParameters,
            Action<SqlDataReader, SqlCommand> readRow,
            Action<SqlCommand>? postAction = null) // <-- added
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            addParameters?.Invoke(cmd);

            try
            {
                await conn.OpenAsync();

                await using (var reader = await cmd.ExecuteReaderAsync())
                {

                while (await reader.ReadAsync())
                {
                    readRow(reader, cmd);
                }
                }

                // Run optional post action (e.g., check output params)
                postAction?.Invoke(cmd);
            }
            catch (Exception ex)
            {
                return ex;
            }

            return null;
        }

        /// <summary>
        /// Execute a stored procedure that modifies data (INSERT/UPDATE/DELETE).
        /// Returns number of affected rows.
        /// </summary>
        public static async Task<int> ExecuteNonQueryAsync(
            string spName,
            Action<SqlCommand>? addParameters,
            Action<SqlCommand>? postAction = null) // <-- added
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            addParameters?.Invoke(cmd);

            await conn.OpenAsync();
            int affected = await cmd.ExecuteNonQueryAsync();

            // Run optional post action (e.g., read OUTPUT params)
            postAction?.Invoke(cmd);

            return affected;
        }

        /// <summary>
        /// Execute a stored procedure and return a single scalar value.
        /// </summary>
        public static async Task<object?> ExecuteScalarAsync(
            string spName,
            Action<SqlCommand>? addParameters,
            Action<SqlCommand>? postAction = null) // <-- added
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            addParameters?.Invoke(cmd);

            await conn.OpenAsync();
            object? result = await cmd.ExecuteScalarAsync();

            // Run optional post action (e.g., check output params)
            postAction?.Invoke(cmd);

            return result;
        }
    }
}
