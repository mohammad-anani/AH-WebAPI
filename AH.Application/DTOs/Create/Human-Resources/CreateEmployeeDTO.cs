using AH.Application.DTOs.Form;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateEmployeeDTO : EmployeeFormDTO
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 10 and 64 characters")]
        public string Password { get; set; }

        [BindNever]
        public int CreatedByAdminID { get; set; }

        public CreateEmployeeDTO() : base()
        {
            Password = string.Empty;
            CreatedByAdminID = -1;
        }

        public Employee ToEmployee()
        {
            var person = new Person(FirstName, MiddleName, LastName, Gender, BirthDate, new Country(CountryID), Phone, new User(Email, Password));
            return new Employee(person, new Department(DepartmentID),
                Salary, HireDate, Employee.ToBitmask(WorkingDays), ShiftStart, ShiftEnd, new AdminAudit(CreatedByAdminID));
        }
    }
}