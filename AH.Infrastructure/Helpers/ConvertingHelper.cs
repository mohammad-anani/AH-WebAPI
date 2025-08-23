using Microsoft.Data.SqlClient;
using System;

namespace AH.Infrastructure.Helpers
{
    public class ConvertingHelper
    {
        private readonly SqlDataReader _reader;

        public ConvertingHelper(SqlDataReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        /// <summary>
        /// Convert a column value from the associated SqlDataReader to the specified type T.
        /// Throws an exception if the conversion fails or value is DBNull for non-nullable types.
        /// </summary>
        public T ConvertValue<T>(string columnName)
        {
            var value = _reader[columnName];

            if (value == DBNull.Value)
            {
                // Return default for nullable types or reference types
                if (Nullable.GetUnderlyingType(typeof(T)) != null || !typeof(T).IsValueType)
                    return default!;

                throw new InvalidOperationException($"Column '{columnName}' is NULL but target type '{typeof(T).Name}' is not nullable.");
            }

            // Convert to target type (handles nullable underlying type)
            return (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
        }
    }
}