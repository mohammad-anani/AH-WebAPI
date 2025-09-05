using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateDoctorDTO : CreateEmployeeDTO
    {
        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Specialization must be between 5 and 100 characters")]
        public string Specialization { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cost per appointment is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Cost per appointment must be a positive number")]
        public int CostPerAppointment { get; set; }

        public CreateDoctorDTO() : base()
        {
            Password = string.Empty;
        }

        public Doctor ToDoctor()
        {
            var person = new Person(FirstName, MiddleName, LastName, Gender, BirthDate, new Country(CountryID), Phone, new User(Email, Password));
            var employee = new Employee(person, new Department(DepartmentID),
                Salary, HireDate, Employee.ToBitmask(WorkingDays), ShiftStart, ShiftEnd, new AdminAudit(CreatedByAdminID ?? -1, new EmployeeAudit()));
            return new Doctor(employee, CostPerAppointment, Specialization);
        }
    }
}