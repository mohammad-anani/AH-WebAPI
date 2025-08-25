using AH.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Helpers
{
    public static class ReceptionistAuditHelper
    {
        public static void AddReceptionistAuditParameters(int? recepID, DateTime? createdAtFrom, DateTime? createdAtTo, SqlCommand cmd)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["CreatedByReceptionistID"] = (recepID, SqlDbType.Int, null, null),
                ["CreatedAtFrom"] = (createdAtFrom, SqlDbType.DateTime, null, null),
                ["CreatedAtTo"] = (createdAtTo, SqlDbType.DateTime, null, null),
            };
            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
        }

        public static Receptionist ReadReceptionist(SqlDataReader reader)
        {
            var converter = new ConvertingHelper(reader);
            Receptionist CreatedByReceptionist = new Receptionist()
            {
                ID = converter.ConvertValue<int>("CreatedByReceptionistID"),
                Employee = {
                    Person =
                        {
                        FirstName = converter.ConvertValue<string>("CreatedByReceptionistFirstName"),
                        MiddleName = converter.ConvertValue<string>("CreatedByReceptionistMiddleName"),
                        LastName = converter.ConvertValue<string>("CreatedByReceptionistLastName"),
                        }
                }
            };
            return CreatedByReceptionist;
        }
    }
}