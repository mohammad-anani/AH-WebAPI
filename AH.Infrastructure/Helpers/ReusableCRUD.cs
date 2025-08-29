using AH.Application.DTOs.Filter.Helpers;
using AH.Application.DTOs.Response;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Threading;

namespace AH.Infrastructure
{
    public static class ReusableCRUD
    {
        public static async Task<GetAllResponseDTO<T>> GetAllAsync<T, B>(
            string spName,
            ILogger logger,
            IFilterable filterDTO,
            Action<SqlCommand>? addParams,
            Func<SqlDataReader, ConvertingHelper, T> readAsync,
            Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>? extraParameters,
            CancellationToken cancellationToken = default)
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
                 (reader, cmd) => { converter = new ConvertingHelper(reader); },
                 cancellationToken);

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

        public static async Task<GetByIDResponseDTO<T>> GetByID<T>(
            string spName,
            ILogger logger,
            int ID,
            Action<SqlCommand>? addParams,
            Func<SqlDataReader, ConvertingHelper, T> constructObject,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                { "@ID", (ID, SqlDbType.Int, null, null) }
            };

            T item = default!;
            ConvertingHelper converter = new ConvertingHelper();

            bool notFound = true;

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
                 (reader, cmd) =>
                 {
                     converter = new ConvertingHelper(reader);
                     notFound = false;
                 },
                 cancellationToken);

            if (notFound)
            {
                ex = new KeyNotFoundException($"Record for ID = {ID} was not found");
            }

            return new GetByIDResponseDTO<T>(item, ex);
        }

        public static async Task<DeleteResponseDTO> DeleteAsync(string spName, ILogger logger, int ID, CancellationToken cancellationToken = default)
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
                 }, null, cancellationToken);

            bool success = successParam.GetResult();

            if (ex != null && (ex.Message.Contains("does not exist") || ex.Message.Contains("not found") || ex.Message.Contains("Invalid") && ex.Message.Contains("ID")))
            {
                ex = new KeyNotFoundException($"Record for ID = {ID} was not found");
            }

            return new DeleteResponseDTO(success, ex);
        }

        public static async Task<SuccessResponseDTO> ExecuteByIDAsync(string spName, ILogger logger, int ID,
             Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>? extraParams,
             CancellationToken cancellationToken = default)
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
                 }, null, cancellationToken);

            if (ex != null && (ex.Message.Contains("does not exist") || ex.Message.Contains("not found") || ex.Message.Contains("Invalid") && ex.Message.Contains("ID")))
            {
                ex = new KeyNotFoundException($"Record for ID = {ID} was not found");
            }

            bool success = successParam.GetResult();

            return new SuccessResponseDTO(success, ex);
        }

        public static async Task<CreateResponseDTO> AddAsync(
     string spName,
     ILogger logger,
     Action<SqlCommand>? addParams = null,
     CancellationToken cancellationToken = default)
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
                null,
                cancellationToken
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

        public static async Task<SuccessResponseDTO> UpdateAsync(
 string spName,
 ILogger logger, int id,
 Action<SqlCommand>? addParams = null,
 CancellationToken cancellationToken = default)
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
                null,
                cancellationToken
            );

            var success = successOutputHelper.GetResult();

            if (ex != null && (ex.Message.Contains("does not exist") || ex.Message.Contains("not found") || ex.Message.Contains("Invalid") && ex.Message.Contains("ID")))
            {
                ex = new KeyNotFoundException($"Record for ID = {id} was not found");
            }

            return new SuccessResponseDTO(success, ex);
        }
    }
}