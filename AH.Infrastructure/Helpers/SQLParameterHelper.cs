using Microsoft.Data.SqlClient;
using System.Data;

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