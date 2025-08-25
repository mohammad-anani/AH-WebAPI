using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Filter.Helpers;

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

namespace AH.Infrastructure
{
    public static class ReusableCRUD
    {
        public static async Task<GetAllResponseDTO<T>> GetAllAsync<T, B>(string spName, ILogger logger, IFilterable filterDTO, Action<SqlCommand>?
            addParams, Func<SqlDataReader,
                ConvertingHelper, T> readAsync,
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
                         SqlParameterHelper.AddParametersFromDictionary(cmd, extraParameters);
                     addParams?.Invoke(cmd);
                     FilterableHelper.AddFilterParameters(filterDTO.Sort, filterDTO.Order, filterDTO.Page, cmd);

                     rowCountOutputHelper.AddToCommand(cmd);
                 }, (reader, cmd) =>
                 {
                     items.Add(readAsync(reader, converter));
                 }, null, (reader, cmd) => { converter = new ConvertingHelper(reader); });

            totalCount = rowCountOutputHelper.GetRowCount();

            return new GetAllResponseDTO<T>(items, totalCount, ex);
        }

        public static async Task<GetByIDResponseDTO<T>> GetByID<T>(string spName, ILogger logger, int ID, Action<SqlCommand>?
            addParams, Func<SqlDataReader, ConvertingHelper, T> constructObject)
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
                 }, (reader, cmd) =>
                 {
                     item = constructObject(reader, converter);
                 }, null, (reader, cmd) => { converter = new ConvertingHelper(reader); });

            return new GetByIDResponseDTO<T>(item, ex);
        }
    }
}