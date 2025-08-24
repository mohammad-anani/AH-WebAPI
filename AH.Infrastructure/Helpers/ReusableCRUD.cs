using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Infrastructure.Helpers;
using AH.Application.DTOs.Filter.Helpers;

namespace AH.Infrastructure
{
    public static class ReusableCRUD
    {
        public static async Task<ListResponseDTO<T>> GetAllAsync<T, B>(string spName, ILogger logger, IFilterable filterDTO, Action<SqlCommand>?
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

            return new ListResponseDTO<T>(items, totalCount, ex);
        }
    }
}