using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AH.Infrastructure.Helpers
{
    public static class SqlParameterHelper
    {
        public static void AddParametersFromDictionary(
            SqlCommand cmd,
            Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)> parameters)
        {
            foreach (var kvp in parameters)
            {
                var paramName = kvp.Key.StartsWith("@") ? kvp.Key : "@" + kvp.Key;
                var value = kvp.Value.Value ?? DBNull.Value;

                var sqlParam = kvp.Value.Size.HasValue
                    ? new SqlParameter(paramName, kvp.Value.Type, kvp.Value.Size.Value)
                    : new SqlParameter(paramName, kvp.Value.Type);

                sqlParam.Value = value;
                sqlParam.Direction = kvp.Value.Direction ?? ParameterDirection.Input;

                cmd.Parameters.Add(sqlParam);
            }
        }
    }
}
