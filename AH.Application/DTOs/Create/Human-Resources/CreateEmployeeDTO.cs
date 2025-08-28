using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateEmployeeDTO : CreatePersonDTO
    {
        public int DepartmentID { get; set; }
        public int Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public int WorkingDays { get; set; }
        public TimeOnly ShiftStart { get; set; }
        public TimeOnly ShiftEnd { get; set; }
        public int CreatedByAdminID { get; set; }

        public CreateEmployeeDTO() : base()
        {
            DepartmentID = -1;
            Salary = 0;
            HireDate = DateTime.MinValue;
            LeaveDate = null;
            WorkingDays = 0;
            ShiftStart = TimeOnly.MinValue;
            ShiftEnd = TimeOnly.MinValue;
            CreatedByAdminID = -1;
        }

        public Employee ToEmployee()
        {
            return new Employee(base.ToPerson(), new Department(DepartmentID),
                Salary, HireDate, LeaveDate, WorkingDays, ShiftStart, ShiftEnd, new AdminAudit(CreatedByAdminID));
        }
    }
}