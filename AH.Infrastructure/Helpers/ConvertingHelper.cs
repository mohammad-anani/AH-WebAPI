using Microsoft.Data.SqlClient;

namespace AH.Infrastructure.Helpers
{
    /// <summary>
    /// Helper class for safely converting SqlDataReader column values to strongly-typed objects.
    /// Handles nullable types, DBNull values, and type conversions with appropriate error handling.
    /// </summary>
    public class ConvertingHelper
    {
        private readonly SqlDataReader _reader;

        public ConvertingHelper(SqlDataReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public ConvertingHelper()
        {
            _reader = null!;
        }

        /// <summary>
        /// Converts a column value from the associated SqlDataReader to the specified type T.
        /// Handles nullable types, DBNull values, and performs safe type conversion.
        /// </summary>
        public T ConvertValue<T>(string columnName)
        {
            var value = _reader[columnName];

            if (value == DBNull.Value)
            {
                if (Nullable.GetUnderlyingType(typeof(T)) != null || !typeof(T).IsValueType)
                    return default!;
                throw new InvalidOperationException(
                    $"Column '{columnName}' is NULL but target type '{typeof(T).Name}' is not nullable.");
            }

            var targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            if (targetType == typeof(Guid))
                return (T)(object)(Guid)value;

            if (targetType == typeof(byte[]))
                return (T)(object)(byte[])value;

            if (targetType == typeof(TimeSpan))
                return (T)(object)(TimeSpan)value;

            if (targetType == typeof(TimeOnly))
                return (T)(object)TimeOnly.FromTimeSpan((TimeSpan)value);

            if (targetType == typeof(DateOnly))
                return (T)(object)DateOnly.FromDateTime((DateTime)value);

            if (targetType.IsEnum)
                return (T)Enum.ToObject(targetType, value);

            return (T)Convert.ChangeType(value, targetType);
        }
    }
}