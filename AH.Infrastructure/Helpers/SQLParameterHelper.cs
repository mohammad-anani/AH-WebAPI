using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace AH.Infrastructure.Helpers
{
    /// <summary>
    /// Utility class for standardizing SQL parameter creation and management.
    /// Provides methods to safely add parameters to SqlCommand objects with proper type handling.
    /// </summary>
    public static class SqlParameterHelper
    {
        /// <summary>
        /// Adds multiple parameters to a SqlCommand from a dictionary definition.
        /// Handles parameter naming, type specification, size constraints, and direction settings.
        /// </summary>
        /// <param name="cmd">The SqlCommand to add parameters to</param>
        /// <param name="parameters">
        /// Dictionary containing parameter definitions where:
        /// - Key: Parameter name (@ prefix is added automatically if missing)
        /// - Value: Tuple containing (Value, SqlDbType, Size, Direction)
        /// </param>
        /// <remarks>
        /// Parameter value handling:
        /// - Null values are automatically converted to DBNull.Value
        /// - Parameter direction defaults to Input if not specified
        /// - Size is optional and only applied when specified
        ///
        /// Parameter naming:
        /// - Automatically adds "@" prefix if not present in the parameter name
        /// - Parameter names should match stored procedure parameter names
        ///
        /// Type safety:
        /// - Uses strongly-typed SqlDbType for parameter definition
        /// - Handles size constraints for variable-length types (NVarChar, VarChar, etc.)
        /// </remarks>
        /// <example>
        /// var parameters = new Dictionary&lt;string, (object?, SqlDbType, int?, ParameterDirection?)&gt;
        /// {
        ///     ["Name"] = ("John Doe", SqlDbType.NVarChar, 100, null),
        ///     ["Age"] = (25, SqlDbType.Int, null, null),
        ///     ["IsActive"] = (true, SqlDbType.Bit, null, null)
        /// };
        /// SqlParameterHelper.AddParametersFromDictionary(command, parameters);
        /// </example>
        public static void AddParametersFromDictionary(
            SqlCommand cmd,
            Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)> parameters)
        {
            // Track duplicates within this call (normalized with '@', case-insensitive)
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var kvp in parameters)
            {
                var originalName = kvp.Key?.Trim() ?? throw new ArgumentException("Parameter name cannot be null", nameof(parameters));
                var paramName = originalName.StartsWith("@") ? originalName : "@" + originalName;

                // 9 - Reject duplicate parameter names (including variations with/without '@')
                if (!seen.Add(paramName) || cmd.Parameters.Contains(paramName))
                {
                    throw new ArgumentException($"Duplicate parameter name detected: {paramName}");
                }

                var (valueObj, sqlType, size, direction) = kvp.Value;
                var value = valueObj ?? DBNull.Value;

                // 5 - Size must be specified for NVarchar/VarChar/Char types
                if (sqlType is SqlDbType.NVarChar or SqlDbType.VarChar or SqlDbType.Char)
                {
                    if (!size.HasValue || size.Value <= 0)
                        throw new ArgumentException($"Size must be specified and > 0 for type {sqlType} (parameter {paramName})");
                }

                // 7 - Validate value type when not null/DBNull
                if (value is not DBNull)
                {
                    ValidateValueMatchesSqlDbType(paramName, sqlType, value);

                    // 8 - Strings must respect specified length when provided
                    if (value is string s && size.HasValue)
                    {
                        if (s.Length > size.Value)
                            throw new ArgumentException($"String length {s.Length} exceeds declared size {size.Value} for {paramName}");
                    }
                }

                var sqlParam = size.HasValue
                    ? new SqlParameter(paramName, sqlType, size.Value)
                    : new SqlParameter(paramName, sqlType);

                sqlParam.Value = value;
                sqlParam.Direction = direction ?? ParameterDirection.Input; // 2 - default to Input, 6 - preserve provided Output

                cmd.Parameters.Add(sqlParam);
            }
        }

        private static void ValidateValueMatchesSqlDbType(string name, SqlDbType type, object value)
        {
            // This is intentionally strict to enforce correctness per documented rules.
            // Expand as needed for additional SqlDbType mappings.
            bool ok = type switch
            {
                SqlDbType.Int => value is int,
                SqlDbType.BigInt => value is long,
                SqlDbType.SmallInt => value is short,
                SqlDbType.TinyInt => value is byte,
                SqlDbType.Bit => value is bool,
                SqlDbType.Date or SqlDbType.DateTime or SqlDbType.DateTime2 or SqlDbType.SmallDateTime => value is DateTime,
                SqlDbType.Decimal or SqlDbType.Money or SqlDbType.SmallMoney => value is decimal,
                SqlDbType.Float => value is double,
                SqlDbType.Real => value is float,
                SqlDbType.UniqueIdentifier => value is Guid,
                SqlDbType.Binary or SqlDbType.VarBinary or SqlDbType.Image => value is byte[],
                SqlDbType.NVarChar or SqlDbType.VarChar or SqlDbType.Char or SqlDbType.NChar or SqlDbType.NText or SqlDbType.Text => value is string,
                _ => true // do not over-restrict unknown mappings
            };

            if (!ok)
            {
                throw new ArgumentException($"Value for parameter {name} does not match SqlDbType {type}. Actual CLR type: {value.GetType().FullName}");
            }
        }
    }
}