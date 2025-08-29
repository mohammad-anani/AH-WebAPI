using AH.Application.DTOs.Row;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AH.Infrastructure.Helpers
{
    public class AdminAuditHelper
    {
        public static void AddAdminAuditParameters(int? adminID, DateTime? createdAtFrom, DateTime? createdAtTo, SqlCommand cmd)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["CreatedByAdminID"] = (adminID, SqlDbType.Int, null, null),
                ["CreatedAtFrom"] = (createdAtFrom, SqlDbType.DateTime, null, null),
                ["CreatedAtTo"] = (createdAtTo, SqlDbType.DateTime, null, null),
            };
            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
        }

        public static AdminRowDTO ReadAdmin(SqlDataReader reader)
        {
            var converter = new ConvertingHelper(reader);

            AdminRowDTO CreatedByAdmin = new AdminRowDTO(

                 converter.ConvertValue<int>("CreatedByAdminID"),

                        converter.ConvertValue<string>("CreatedByAdminFullName")

            );

            return CreatedByAdmin;
        }
    }
}