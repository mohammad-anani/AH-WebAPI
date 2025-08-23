using Microsoft.Data.SqlClient;
using System.Data;

namespace AH.Infrastructure.Helpers
{
    public static class FilterableHelper
    {
        public static void AddFilterParameters(string? sort, bool? order, int? page, SqlCommand cmd)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Page"] = (page, SqlDbType.Int, null, null),
                ["Sort"] = (sort, SqlDbType.NVarChar, 20, null),
                ["Order"] = (order, SqlDbType.Bit, null, null),
            };
            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
        }
    }
}