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
    public static class ReusableCRUD
    {
        public static async Task<GetAllResponseDTO<T>> GetAllAsync<T, B>(
            string spName,
            ILogger logger,
            IFilterable filterDTO,
            Action<SqlCommand>? addParams,
            Func<SqlDataReader, ConvertingHelper, T> readAsync,
            Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>? extraParameters)
        {
            logger.LogInformation("Executing {StoredProcedure} with paging filters: Sort={Sort}, Order={Order}, Page={Page}",
                spName, filterDTO.Sort, filterDTO.Order, filterDTO.Page);

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
                         logger.LogDebug("Added extra parameters: {Parameters}", extraParameters.Keys);
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
                logger.LogError(ex, "Error executing {StoredProcedure}", spName);
            else
                logger.LogInformation("Successfully executed {StoredProcedure}. Returned {Count} rows", spName, totalCount);

            return new GetAllResponseDTO<T>(items, totalCount, ex);
        }

        public static async Task<GetByIDResponseDTO<T>> GetByID<T>(
            string spName,
            ILogger logger,
            int ID,
            Action<SqlCommand>? addParams,
            Func<SqlDataReader, ConvertingHelper, T> constructObject)
        {
            logger.LogInformation("Executing {StoredProcedure} with ID={ID}", spName, ID);

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

            if (ex != null)
                logger.LogError(ex, "Error executing {StoredProcedure} with ID={ID}", spName, ID);
            else
                logger.LogInformation("Successfully executed {StoredProcedure} with ID={ID}", spName, ID);

            return new GetByIDResponseDTO<T>(item, ex);
        }

        public static async Task<DeleteResponseDTO> DeleteAsync(string spName, ILogger logger, int ID)
        {
            logger.LogInformation("Executing {StoredProcedure} for Delete with ID={ID}", spName, ID);

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

            if (ex != null)
                logger.LogError(ex, "Error executing {StoredProcedure} for Delete with ID={ID}", spName, ID);
            else
                logger.LogInformation("Successfully executed {StoredProcedure} for Delete with ID={ID}, Success={Success}", spName, ID, success);

            return new DeleteResponseDTO(success, ex);
        }

        public static async Task<SuccessResponseDTO> ExecuteByIDAsync(string spName, ILogger logger, int ID,
             Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>? extraParams)
        {
            logger.LogInformation("Executing {StoredProcedure} with ID={ID}", spName, ID);

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
                         SqlParameterHelper.AddParametersFromDictionary(cmd, extraParams);
                 }, null);

            bool success = successParam.GetResult();

            if (ex != null)
                logger.LogError(ex, "Error executing {StoredProcedure} with ID={ID}", spName, ID);
            else
                logger.LogInformation("Successfully executed {StoredProcedure} with ID={ID}, Success={Success}", spName, ID, success);

            return new SuccessResponseDTO(success, ex);
        }

        public static async Task<CreateResponseDTO> AddAsync(
     string spName,
     ILogger logger,
     Action<SqlCommand>? addParams = null)
        {
            logger.LogInformation("Executing {StoredProcedure} for Add operation", spName);

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
                logger.LogError(ex, "Error executing {StoredProcedure} for Add operation", spName);
            else
                logger.LogInformation("Successfully executed {StoredProcedure}. New ID = {NewId}", spName, newId);

            return new CreateResponseDTO(newId, ex);
        }

        public static async Task<SuccessResponseDTO> UpdateAsync(
 string spName,
 ILogger logger, int id,
 Action<SqlCommand>? addParams = null)
        {
            logger.LogInformation("Executing {StoredProcedure} for Update operation", spName);

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

            if (ex != null)
                logger.LogError(ex, "Error executing {StoredProcedure} for Add operation", spName);
            else
                logger.LogInformation("Successfully executed {StoredProcedure}.", spName);

            return new SuccessResponseDTO(success, ex);
        }
    }
}