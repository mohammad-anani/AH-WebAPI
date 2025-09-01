using AH.Application.DTOs.Filter.Helpers;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System.Runtime.InteropServices;

namespace AH.Application.DTOs.Filter
{
    public class EmployeeFilter : PersonFilter, IAdminAudit
    {
        public int? DepartmentID { get; set; }
        public int? SalaryFrom { get; set; }
        public int? SalaryTo { get; set; }
        public DateTime? HireDateFrom { get; set; }
        public DateTime? HireDateTo { get; set; }
        public DateTime? LeaveDateFrom { get; set; }
        public DateTime? LeaveDateTo { get; set; }
        public TimeSpan? ShiftStartFrom { get; set; }
        public TimeSpan? ShiftStartTo { get; set; }
        public TimeSpan? ShiftEndFrom { get; set; }
        public TimeSpan? ShiftEndTo { get; set; }

        [WorkingDaysString(false)]
        public string? WorkingDays { get; set; }

        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
        public int? CreatedByAdminID { get; set; }

        // Full constructor
        public EmployeeFilter(
            int? departmentID,
            int? salaryFrom,
            int? salaryTo,
            DateTime? hireDateFrom,
            DateTime? hireDateTo,
            DateTime? leaveDateFrom,
            DateTime? leaveDateTo,
            TimeSpan? shiftStartFrom,
            TimeSpan? shiftStartTo,
            TimeSpan? shiftEndFrom,
            TimeSpan? shiftEndTo,
           string? workingDays,
            DateTime? createdAtFrom,
            DateTime? createdAtTo,
            int? createdByAdminID)
        {
            DepartmentID = departmentID;
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
        {
        }
    }
}