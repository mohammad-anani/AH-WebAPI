using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Filter.Helpers;
using AH.Application.DTOs.Response;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace AH.Infrastructure
{
    /// <summary>
    /// Provides reusable CRUD operations for database entities using stored procedures.
    /// Standardizes database access patterns with consistent logging, error handling, and parameter management.
    /// </summary>
    public static class ReusableCRUD
    {
        /// <summary>
        /// Executes a stored procedure to retrieve a paginated list of entities with filtering and sorting support.
        /// </summary>
        /// <typeparam name="T">The type of entity to return in the result set</typeparam>
        /// <typeparam name="B">The type of filter DTO (currently unused but maintained for interface consistency)</typeparam>
        /// <param name="spName">The name of the stored procedure to execute</param>
        /// <param name="logger">Logger instance for structured logging</param>
        /// <param name="filterDTO">Filter criteria including pagination, sorting, and entity-specific filters</param>
        /// <param name="addParams">Optional action to add custom parameters to the SQL command</param>
        /// <param name="readAsync">Function to construct entity objects from SqlDataReader rows</param>
        /// <param name="extraParameters">Optional dictionary of additional parameters for the stored procedure</param>
        /// <returns>Response containing the list of entities, total count, and any exception that occurred</returns>
        /// <remarks>
        /// Automatically adds pagination parameters (Sort, Order, Page) and row count output parameter.
        /// The stored procedure should support these standard parameters and return a row count.
        /// Logs at Information level for successful operations and Error level for failures.
        /// </remarks>
        public static async Task<GetAllResponseDTO<T>> GetAllAsync<T, B>(
            string spName,
            ILogger logger,
            IFilterable filterDTO,
            Action<SqlCommand>? addParams,
            Func<SqlDataReader, ConvertingHelper, T> readAsync,
            Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>? extraParameters)
        {
            int totalCount = -1;
            List<T> items = new List<T>();
            ConvertingHelper converter = new ConvertingHelper();
            RowCountOutputHelper rowCountOutputHelper = new RowCountOutputHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 spName, logger, cmd =>
                 {
                     if (extraParameters != null)
                     {
                         SqlParameterHelper.AddParametersFromDictionary(cmd, extraParameters);
                     }

                     addParams?.Invoke(cmd);
                     FilterableHelper.AddFilterParameters(filterDTO.Sort, filterDTO.Order, filterDTO.Page, cmd);
                     rowCountOutputHelper.AddToCommand(cmd);
                 },
                 (reader, cmd) =>
                 {
                     items.Add(readAsync(reader, converter));
                 },
                 null,
                 (reader, cmd) => { converter = new ConvertingHelper(reader); });

            totalCount = rowCountOutputHelper.GetRowCount();

            if (ex != null)
            {
                logger.LogError(ex, "Failed to execute {StoredProcedure}", spName);
            }
            else
            {
                logger.LogInformation("Successfully executed {StoredProcedure} - Retrieved {Count} rows",
                    spName, totalCount);
            }

            return new GetAllResponseDTO<T>(items, totalCount, ex);
        }

        /// <summary>
        /// Executes a stored procedure to retrieve a single entity by its unique identifier.
        /// </summary>
        /// <typeparam name="T">The type of entity to return</typeparam>
        /// <param name="spName">The name of the stored procedure to execute</param>
        /// <param name="logger">Logger instance for structured logging</param>
        /// <param name="ID">The unique identifier of the entity to retrieve</param>
        /// <param name="addParams">Optional action to add custom parameters to the SQL command</param>
        /// <param name="constructObject">Function to construct the entity object from SqlDataReader</param>
        /// <returns>Response containing the entity or an exception if the operation failed</returns>
        /// <remarks>
        /// Automatically adds the @ID parameter to the stored procedure call.
        /// The stored procedure should expect an @ID parameter and return a single row or no rows.
        /// Logs at Information level for successful operations and Error level for failures.
        /// </remarks>
        public static async Task<GetByIDResponseDTO<T>> GetByID<T>(
            string spName,
            ILogger logger,
            int ID,
            Action<SqlCommand>? addParams,
            Func<SqlDataReader, ConvertingHelper, T> constructObject)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "@ID", (ID, SqlDbType.Int, null, null) }
            };

            T item = default!;
            ConvertingHelper converter = new ConvertingHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 spName, logger, cmd =>
                 {
                     SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
                     addParams?.Invoke(cmd);
                 },
                 (reader, cmd) =>
                 {
                     item = constructObject(reader, converter);
                 },
                 null,
                 (reader, cmd) => { converter = new ConvertingHelper(reader); });

            return new GetByIDResponseDTO<T>(item, ex);
        }

        /// <summary>
        /// Executes a stored procedure to permanently delete an entity from the database.
        /// </summary>
        /// <param name="spName">The name of the delete stored procedure to execute</param>
        /// <param name="logger">Logger instance for structured logging</param>
        /// <param name="ID">The unique identifier of the entity to delete</param>
        /// <returns>Response indicating success/failure and any exception that occurred</returns>
        /// <remarks>
        /// This is a destructive operation that permanently removes data from the database.
        /// Automatically adds @ID parameter and success output parameter to the stored procedure call.
        /// Logs at Warning level due to the destructive nature of delete operations.
        /// Consider using leave/deactivate operations for non-destructive alternatives.
        /// </remarks>
        public static async Task<DeleteResponseDTO> DeleteAsync(string spName, ILogger logger, int ID)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "@ID", (ID, SqlDbType.Int, null, null) }
            };

            SuccessOutputHelper successParam = new SuccessOutputHelper();

            Exception? ex = await ADOHelper.ExecuteNonQueryAsync(
                 spName, logger, cmd =>
                 {
                     successParam.AddToCommand(cmd);
                     SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
                 }, null);

            bool success = successParam.GetResult();

            return new DeleteResponseDTO(success, ex);
        }

        /// <summary>
        /// Executes a stored procedure with an ID parameter and optional additional parameters.
        /// Commonly used for operations like leave, activate, deactivate, or other ID-based actions.
        /// </summary>
        /// <param name="spName">The name of the stored procedure to execute</param>
        /// <param name="logger">Logger instance for structured logging</param>
        /// <param name="ID">The unique identifier for the operation</param>
        /// <param name="extraParams">Optional dictionary of additional parameters for the stored procedure</param>
        /// <returns>Response indicating success/failure and any exception that occurred</returns>
        /// <remarks>
        /// Automatically adds @ID parameter and success output parameter to the stored procedure call.
        /// Used for non-destructive operations like setting leave dates, status changes, etc.
        /// Logs at Information level for successful operations and Error level for failures.
        /// </remarks>
        public static async Task<SuccessResponseDTO> ExecuteByIDAsync(string spName, ILogger logger, int ID,
             Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>? extraParams)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "@ID", (ID, SqlDbType.Int, null, null) }
            };

            SuccessOutputHelper successParam = new SuccessOutputHelper();

            Exception? ex = await ADOHelper.ExecuteNonQueryAsync(
                 spName, logger, cmd =>
                 {
                     successParam.AddToCommand(cmd);
                     SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

                     if (extraParams != null)
                     {
                         SqlParameterHelper.AddParametersFromDictionary(cmd, extraParams);
                         logger.LogDebug("Added {ParameterCount} extra parameters to {StoredProcedure}",
                             extraParams.Count, spName);
                     }
                 }, null);

            bool success = successParam.GetResult();

            return new SuccessResponseDTO(success, ex);
        }

        /// <summary>
        /// Executes a stored procedure to create a new entity in the database.
        /// </summary>
        /// <param name="spName">The name of the create stored procedure to execute</param>
        /// <param name="logger">Logger instance for structured logging</param>
        /// <param name="addParams">Optional action to add entity-specific parameters to the SQL command</param>
        /// <returns>Response containing the new entity ID or an exception if the operation failed</returns>
        /// <remarks>
        /// Automatically adds an output parameter to capture the newly created entity's ID.
        /// The stored procedure should return the new ID through an output parameter.
        /// All business logic and validation should be handled within the stored procedure.
        /// Logs at Information level for successful operations and Error level for failures.
        /// </remarks>
        public static async Task<CreateResponseDTO> AddAsync(
     string spName,
     ILogger logger,
     Action<SqlCommand>? addParams = null)
        {
            IdOutputHelper idOutputHelper = new IdOutputHelper();

            Exception? ex = await ADOHelper.ExecuteNonQueryAsync(
                spName,
                logger,
                cmd =>
                {
                    idOutputHelper.AddToCommand(cmd);
                    addParams?.Invoke(cmd); // caller adds parameters here
                },
                null
            );

            var newId = idOutputHelper.GetNewID();

            if (ex != null)
            {
                logger.LogError(ex, "Failed CREATE operation {StoredProcedure}", spName);
            }
            else
            {
                logger.LogInformation("CREATE operation successful {StoredProcedure} - New ID: {NewId}",
                    spName, newId);
            }

            return new CreateResponseDTO(newId, ex);
        }

        /// <summary>
        /// Executes a stored procedure to update an existing entity in the database.
        /// </summary>
        /// <param name="spName">The name of the update stored procedure to execute</param>
        /// <param name="logger">Logger instance for structured logging</param>
        /// <param name="id">The unique identifier of the entity to update</param>
        /// <param name="addParams">Optional action to add entity-specific parameters to the SQL command</param>
        /// <returns>Response indicating success/failure and any exception that occurred</returns>
        /// <remarks>
        /// Automatically adds @ID parameter and success output parameter to the stored procedure call.
        /// The stored procedure should expect an @ID parameter and return a success indicator.
        /// All business logic and validation should be handled within the stored procedure.
        /// Logs at Information level for successful operations and Error level for failures.
        /// </remarks>
        public static async Task<SuccessResponseDTO> UpdateAsync(
 string spName,
 ILogger logger, int id,
 Action<SqlCommand>? addParams = null)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "@ID", (id, SqlDbType.Int, null, null) }
            };

            SuccessOutputHelper successOutputHelper = new SuccessOutputHelper();

            Exception? ex = await ADOHelper.ExecuteNonQueryAsync(
                spName,
                logger,
                cmd =>
                {
                    successOutputHelper.AddToCommand(cmd);
                    addParams?.Invoke(cmd);
                    SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);// caller adds parameters here
                },
                null
            );

            var success = successOutputHelper.GetResult();

            return new SuccessResponseDTO(success, ex);
        }
    }
}