namespace AH.Application.DTOs.Filter
{
    public class OperationFilterDTO : ServiceFilter, IFilterable
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? DepartmentID { get; set; }
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public OperationFilterDTO(
            string? name,
            string? description,
            int? departmentID,
            string? sort,
            bool? order,
            int? page,
            int? patientId = null,
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
            int? billId = null,
            int? createdByReceptionistId = null,
            DateTime? createdAtFrom = null,
            DateTime? createdAtTo = null)
            : base(patientId, scheduledDateFrom, scheduledDateTo, actualStartingDateFrom, actualStartingDateTo,
                   reason, result, resultDateFrom, resultDateTo, status, notes, billId, createdByReceptionistId, createdAtFrom, createdAtTo)
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
    }
}