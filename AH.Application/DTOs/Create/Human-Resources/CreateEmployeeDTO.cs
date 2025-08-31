using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateEmployeeDTO : CreatePersonDTO
    {
        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(100, 99999, ErrorMessage = "Salary must be between 100 and 99,999")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Hire date is required")]
        [HireDateValidation]
        public DateTime HireDate { get; set; }

        [WorkingDaysString]
        public string WorkingDays { get; set; }

        [Required(ErrorMessage = "Shift start time is required")]
        public TimeOnly ShiftStart { get; set; }

        [Required(ErrorMessage = "Shift end time is required")]
        public TimeOnly ShiftEnd { get; set; }

        [BindNever]
        public int CreatedByAdminID { get; set; }

        public CreateEmployeeDTO() : base()
        {
            DepartmentID = -1;
            Salary = 0;
            HireDate = DateTime.MinValue;
            WorkingDays = String.Empty;
            ShiftStart = TimeOnly.MinValue;
            ShiftEnd = TimeOnly.MinValue;
            CreatedByAdminID = -1;
        }

        public Employee ToEmployee()
        {
            return new Employee(base.ToPerson(), new Department(DepartmentID),
                Salary, HireDate, Employee.ToBitmask(WorkingDays), ShiftStart, ShiftEnd, new AdminAudit(CreatedByAdminID));
        }
    }
}