namespace AH.Application.DTOs.Filter
{
    public class EmployeeFilter : PersonFilter
    {
        public int? DepartmentId { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public DateTime? HireDateFrom { get; set; }
        public DateTime? HireDateTo { get; set; }
        public DateTime? LeaveDateFrom { get; set; }
        public DateTime? LeaveDateTo { get; set; }
        public TimeSpan? ShiftStartFrom { get; set; }
        public TimeSpan? ShiftStartTo { get; set; }
        public TimeSpan? ShiftEndFrom { get; set; }
        public TimeSpan? ShiftEndTo { get; set; }
        public int? WorkingDays { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
        public int? CreatedByAdminID { get; set; }

        // Full constructor
        public EmployeeFilter(
            int? departmentId,
            decimal? salaryFrom,
            decimal? salaryTo,
            DateTime? hireDateFrom,
            DateTime? hireDateTo,
            DateTime? leaveDateFrom,
            DateTime? leaveDateTo,
            TimeSpan? shiftStartFrom,
            TimeSpan? shiftStartTo,
            TimeSpan? shiftEndFrom,
            TimeSpan? shiftEndTo,
            int? workingDays,
            DateTime? createdAtFrom,
            DateTime? createdAtTo,
            int? createdByAdminID)
        {
            DepartmentId = departmentId;
            SalaryFrom = salaryFrom;
            SalaryTo = salaryTo;
            HireDateFrom = hireDateFrom;
            HireDateTo = hireDateTo;
            LeaveDateFrom = leaveDateFrom;
            LeaveDateTo = leaveDateTo;
            ShiftStartFrom = shiftStartFrom;
            ShiftStartTo = shiftStartTo;
            ShiftEndFrom = shiftEndFrom;
            ShiftEndTo = shiftEndTo;
            WorkingDays = workingDays;
            CreatedAtFrom = createdAtFrom;
            CreatedAtTo = createdAtTo;
            CreatedByAdminID = createdByAdminID;
        }

        // Parameterless constructor
        public EmployeeFilter()
        { }
    }
}