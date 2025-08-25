using AH.Application.DTOs.Filter;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Helpers
{
    public static class ServiceHelper
    {
        public static void AddServiceParameters(ServiceFilter serviceFilter, SqlCommand cmd)
        {
            // Service-specific parameters based on ServiceFilter properties
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["PatientID"] = (serviceFilter.PatientID, SqlDbType.Int, null, null),
                ["ScheduledDateFrom"] = (serviceFilter.ScheduledDateFrom, SqlDbType.DateTime, null, null),
                ["ScheduledDateTo"] = (serviceFilter.ScheduledDateTo, SqlDbType.DateTime, null, null),
                ["ActualStartingDateFrom"] = (serviceFilter.ActualStartingDateFrom, SqlDbType.DateTime, null, null),
                ["ActualStartingDateTo"] = (serviceFilter.ActualStartingDateTo, SqlDbType.DateTime, null, null),
                ["Reason"] = (serviceFilter.Reason, SqlDbType.NVarChar, -1, null),
                ["Result"] = (serviceFilter.Result, SqlDbType.NVarChar, -1, null),
                ["ResultDateFrom"] = (serviceFilter.ResultDateFrom, SqlDbType.DateTime, null, null),
                ["ResultDateTo"] = (serviceFilter.ResultDateTo, SqlDbType.DateTime, null, null),
                ["Status"] = (serviceFilter.Status, SqlDbType.TinyInt, null, null),
                ["Notes"] = (serviceFilter.Notes, SqlDbType.NVarChar, 500, null),
                ["AmountFrom"] = (serviceFilter.AmountFrom, SqlDbType.Decimal, null, null),
                ["AmountTo"] = (serviceFilter.AmountTo, SqlDbType.Decimal, null, null),
                ["AmountPaidFrom"] = (serviceFilter.AmountPaidFrom, SqlDbType.Decimal, null, null),
                ["AmountPaidTo"] = (serviceFilter.AmountPaidTo, SqlDbType.Decimal, null, null),
            };

            SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);

            // Add receptionist audit parameters
            ReceptionistAuditHelper.AddReceptionistAuditParameters(
                serviceFilter.CreatedByReceptionistID,
                serviceFilter.CreatedAtFrom,
                serviceFilter.CreatedAtTo,
                cmd);
        }
    }
}