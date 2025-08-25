using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static AdminAudit ReadAdmin(SqlDataReader reader)
        {
            var converter = new ConvertingHelper(reader);

            AdminAudit CreatedByAdmin = new AdminAudit()
            {
                ID = converter.ConvertValue<int>("CreatedByAdminID"),

                Employee = {
                    Person =
                        {
                        FirstName = converter.ConvertValue<string>("CreatedByAdminFirstName"),
                        MiddleName = converter.ConvertValue<string>("CreatedByAdminMiddleName"),
                        LastName = converter.ConvertValue<string>("CreatedByAdminLastName"),
                        }
                }
            };

            return CreatedByAdmin;
        }
    }
}