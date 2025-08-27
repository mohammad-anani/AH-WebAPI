using AH.Application.DTOs.Row;
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

        public static ReceptionistRowDTO ReadReceptionist(SqlDataReader reader)
        {
            var converter = new ConvertingHelper(reader);
            ReceptionistRowDTO CreatedByReceptionist = new ReceptionistRowDTO(

                  converter.ConvertValue<int>("CreatedByReceptionistID"),

                         converter.ConvertValue<string>("CreatedByReceptionistFullName")

             ); ;
            return CreatedByReceptionist;
        }
    }
}