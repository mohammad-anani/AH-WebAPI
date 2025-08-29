using AH.Application.DTOs.Validation;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateEmployeeDTO : UpdatePersonDTO
    {
        [Required(ErrorMessage = "Employee ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive number")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(100, 99999, ErrorMessage = "Salary must be between 100 and 99,999")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Working days is required")]
        [Range(1, 127, ErrorMessage = "Working days must be between 1 and 127")]
        public int WorkingDays { get; set; }

        [Required(ErrorMessage = "Shift start time is required")]
        public TimeOnly ShiftStart { get; set; }

        [Required(ErrorMessage = "Shift end time is required")]
        public TimeOnly ShiftEnd { get; set; }

        public UpdateEmployeeDTO() : base()
        {
            ID = -1;
            DepartmentID = -1;
            Salary = 0;
            WorkingDays = 0;
            ShiftStart = TimeOnly.MinValue;
            ShiftEnd = TimeOnly.MinValue;
        }

        public Employee ToEmployee()
        {
            return new Employee(base.ToPerson(), new Department(DepartmentID),
                Salary, DateTime.MinValue, WorkingDays, ShiftStart, ShiftEnd, new AdminAudit(-1));
        }
    }
}