using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateDoctorDTO : DoctorFormDTO
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 10 and 64 characters")]
        public string Password { get; set; }

        public CreateDoctorDTO() : base()
        {
            Password = string.Empty;
        }

        public Doctor ToDoctor()
        {
            var person = new Person(FirstName, MiddleName, LastName, Gender, BirthDate, new Country(CountryID), Phone, new User(Email, Password));
            var employee = new Employee(person, new Department(DepartmentID),
                Salary, HireDate, Employee.ToBitmask(WorkingDays), ShiftStart, ShiftEnd, null);
            return new Doctor(employee, CostPerAppointment, Specialization);
        }
    }
}