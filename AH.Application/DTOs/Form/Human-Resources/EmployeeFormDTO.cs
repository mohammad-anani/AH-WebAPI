using AH.Application.DTOs.Validation;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class EmployeeFormDTO : PersonFormDTO
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
        public string WorkingDays { get; set; } = string.Empty;

        [Required(ErrorMessage = "Shift start time is required")]
        public TimeOnly ShiftStart { get; set; }

        [Required(ErrorMessage = "Shift end time is required")]
        public TimeOnly ShiftEnd { get; set; }
    }
}
