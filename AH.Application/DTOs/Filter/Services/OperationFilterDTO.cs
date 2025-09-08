using AH.Application.DTOs.Filter.Helpers;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System.Data;

namespace AH.Application.DTOs.Filter
{
    public class OperationFilterDTO : ServiceFilter, IFilterable
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? DepartmentID { get; set; }

        [OperationDoctors]
        public string? Doctors { get; set; }
        public string? Sort { get; set; }
        public string? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public OperationFilterDTO(
            string? name,
            string? description,
            int? departmentID,
            string? sort,
            string? order,
            int? page,
            int? patientID = null,
            DateTime? scheduledDateFrom = null,
            DateTime? scheduledDateTo = null,
            DateTime? actualStartingDateFrom = null,
            DateTime? actualStartingDateTo = null,
            string? reason = null,
            string? result = null,
            DateTime? resultDateFrom = null,
            DateTime? resultDateTo = null,
            byte? status = null,
            string? notes = null,
            int? amountFrom = null,
            int? amountTo = null,
            int? amountPaidFrom = null,
            int? amountPaidTo = null,
            int? createdByReceptionistID = null,
            DateTime? createdAtFrom = null,
            DateTime? createdAtTo = null)
            : base(patientID, scheduledDateFrom, scheduledDateTo, actualStartingDateFrom, actualStartingDateTo,
                   reason, result, resultDateFrom, resultDateTo, status, notes, amountFrom, amountTo,
                   amountPaidFrom, amountPaidTo, createdByReceptionistID, createdAtFrom, createdAtTo)
        {
            Name = name;
            Description = description;
            DepartmentID = departmentID;
            Sort = sort;
            Order = order;
            Page = page;
        }

        // Parameterless constructor
        public OperationFilterDTO() : base()
        {
            Name = null;
            Description = null;
            DepartmentID = null;
            Sort = null;
            Order = null;
            Page = null;
        }

        public DataTable ToOperationDoctorDatatable()
        {
            var table = new DataTable();
            table.Columns.Add("DoctorID", typeof(int));
            table.Columns.Add("Role", typeof(string));

            if (string.IsNullOrWhiteSpace(Doctors))
                return table;

            // Ensure ending semicolon to simplify parsing
            Doctors = Doctors.Trim();
            if (!Doctors.EndsWith(";")) Doctors += ";";

            int start = 0;
            int sep;

            while ((sep = Doctors.IndexOf(';', start)) >= 0)
            {
                var pair = Doctors[start..sep]; // substring between start and semicolon
                if (!string.IsNullOrWhiteSpace(pair))
                {
                    var parts = pair.Split(':', 2); // split into DoctorID and Role
                    if (parts.Length >= 1 && int.TryParse(parts[0], out int doctorId))
                    {
                        var role = parts.Length == 2 ? parts[1] : null;
                        var row = table.NewRow();
                        row["DoctorID"] = doctorId;
                        row["Role"] = string.IsNullOrWhiteSpace(role) ? DBNull.Value : role;
                        table.Rows.Add(row);
                    }
                }
                start = sep + 1;
            }

            return table;
        }

    }
}