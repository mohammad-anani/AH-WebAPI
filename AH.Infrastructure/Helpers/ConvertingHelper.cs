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

        /// <summary>
        /// Initializes a new instance of ConvertingHelper with an active SqlDataReader.
        /// </summary>
        /// <param name="reader">The SqlDataReader containing the data to convert</param>
        /// <exception cref="ArgumentNullException">Thrown when reader is null</exception>
        public ConvertingHelper(SqlDataReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        /// <summary>
        /// Initializes a new instance of ConvertingHelper without a reader.
        /// This constructor is used when the reader will be set later in the process.
        /// </summary>
        /// <remarks>
        /// ConvertValue methods will fail if called on an instance created with this constructor.
        /// This is typically used in scenarios where the helper is created before the reader is available.
        /// </remarks>
        public ConvertingHelper()
        {
            _reader = null!;
        }

        /// <summary>
        /// Converts a column value from the associated SqlDataReader to the specified type T.
        /// Handles nullable types, DBNull values, and performs safe type conversion.
        /// </summary>
        /// <typeparam name="T">The target type for conversion. Can be nullable, value type, or reference type</typeparam>
        /// <param name="columnName">The name of the database column to retrieve and convert</param>
        /// <returns>The converted value of type T, or default(T) for nullable/reference types when DBNull</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the column contains DBNull but the target type is a non-nullable value type
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown when the column value cannot be converted to the target type
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the column name does not exist in the SqlDataReader
        /// </exception>
        /// <remarks>
        /// This method properly handles:
        /// - Nullable value types (int?, DateTime?, etc.) - returns null for DBNull
        /// - Reference types (string, object, etc.) - returns null for DBNull
        /// - Non-nullable value types (int, DateTime, etc.) - throws exception for DBNull
        /// - Type conversion using Convert.ChangeType for compatible types
        ///
        /// Database column naming conventions should match the provided columnName parameter exactly.
        /// The method uses SqlDataReader indexer which is case-sensitive.
        /// </remarks>
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

            if (targetType.IsEnum)
                return (T)Enum.ToObject(targetType, value);

            return (T)Convert.ChangeType(value, targetType);
        }
    }
}