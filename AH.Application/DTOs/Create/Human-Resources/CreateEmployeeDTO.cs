using AH.Application.DTOs.Form;
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
        public DateOnly HireDate { get; set; }

        [WorkingDaysString]
        public List<string> WorkingDays { get; set; } = new List<string>();

        [Required(ErrorMessage = "Shift start time is required")]
        public TimeOnly ShiftStart { get; set; }

        [Required(ErrorMessage = "Shift end time is required")]
        public TimeOnly ShiftEnd { get; set; }

        [BindNever]
        public int? CreatedByAdminID { get; set; }

        public CreateEmployeeDTO() : base()
        {
            Password = string.Empty;
            CreatedByAdminID = -1;
        }

        public Employee ToEmployee()
        {
            var person = new Person(FirstName, MiddleName, LastName, Gender, BirthDate, new Country(CountryID), Phone, new User(Email, Password));
            return new Employee(person, new Department(DepartmentID),
                Salary, HireDate, Employee.ToBitmask(WorkingDays), ShiftStart, ShiftEnd, CreatedByAdminID != null ? new AdminAudit((int)CreatedByAdminID) : null);
        }
    }
}