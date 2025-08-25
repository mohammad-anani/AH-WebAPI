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
      ILogger logger,
      Action<SqlCommand>? addParameters,
      Action<SqlDataReader, SqlCommand> readRow,
      Action<SqlCommand>? postAction = null,
      Action<SqlDataReader, SqlCommand>? beforeIteration = null)
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (addParameters != null)
            {
                logger.LogDebug("Command parameter count before executing addParameters:{count}", cmd.Parameters.Count);
                addParameters?.Invoke(cmd);
                logger.LogDebug("Command parameter count after executing addParameters:{count}", cmd.Parameters.Count);
            }
            else
            {
                logger.LogDebug("No addParameters action provided, skipping parameter addition.");
            }

            try
            {
                logger.LogInformation("Opening Connection");
                await conn.OpenAsync();

                logger.LogInformation("Starting reader");

                bool loop = false;
                while (!loop)
                {
                    loop = true;
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        logger.LogDebug("Reader has rows after executing reader before iterating:{hasRows}", reader.HasRows);
                        if (!reader.HasRows)
                        {
                            logger.LogWarning("Reader has no rows, exiting early.");
                            break;
                        }

                        if (beforeIteration != null)
                        {
                            logger.LogInformation("Before Iteration Executing.");

                            beforeIteration?.Invoke(reader, cmd);

                            logger.LogInformation("Before Iteration Executed.");
                        }
                        else
                        {
                            logger.LogDebug("No beforeIteration action provided, skipping pre-iteration.");
                        }

                        int rowcount = 0;
                        while (await reader.ReadAsync())
                        {
                            readRow(reader, cmd);
                            rowcount++;
                        }
                        logger.LogDebug("Reader row count after iterating:{count}", rowcount);

                        logger.LogInformation("Closing reader");
                    }
                }

                if (postAction != null)
                {
                    logger.LogInformation("Post action executing");
                    postAction?.Invoke(cmd);
                    logger.LogInformation("Post action executed");
                }
                else
                {
                    logger.LogDebug("No postAction provided, skipping post-action hook.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing stored procedure {spName}", spName);
                return ex;
            }

            logger.LogInformation("Successfully executed stored procedure {spName}", spName);
            return null;
        }

        public static async Task<int> ExecuteNonQueryAsync(
            string spName,
            Action<SqlCommand>? addParameters,
            ILogger logger,
            Action<SqlCommand>? postAction = null)
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            addParameters?.Invoke(cmd);

            await conn.OpenAsync();
            int affected = await cmd.ExecuteNonQueryAsync();

            logger.LogInformation("Post action executing");
            postAction?.Invoke(cmd);
            logger.LogInformation("Post action executed");

            return affected;
        }

        public static async Task<object?> ExecuteScalarAsync(
            string spName,
            Action<SqlCommand>? addParameters,
            ILogger logger,
            Action<SqlCommand>? postAction = null)
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            addParameters?.Invoke(cmd);

            await conn.OpenAsync();
            object? result = await cmd.ExecuteScalarAsync();

            logger.LogInformation("Post action executing");
            postAction?.Invoke(cmd);
            logger.LogInformation("Post action executed");

            return result;
        }
    }
}