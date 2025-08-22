using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Helpers
{
    public static class ADOHelper
    {
        static string _connectionString=ConfigHelper.GetConnectionString();

        public static async Task<Exception?> ExecuteReaderAsync(
       string spName,
       Action<SqlCommand> addParameters,
       Func<SqlDataReader, Task> readRowAsync)
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Add parameters via delegate
            addParameters?.Invoke(cmd);

            try
            {
                await conn.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    await readRowAsync(reader);
                }
            }
            catch (Exception ex)
            {
                return ex;
            }

            return null;
        }


        // Reusable method for SP that returns affected rows
        public static int ExecuteNonQuery(string spName, Action<SqlCommand> addParameters)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            addParameters?.Invoke(cmd);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        // Reusable method for SP with output parameter
        public static T ExecuteScalar<T>(string spName, Action<SqlCommand> addParameters)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            addParameters?.Invoke(cmd);

            conn.Open();
            return (T)cmd.ExecuteScalar();
        }
    }
}
