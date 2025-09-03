using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateDoctorDTO : DoctorFormDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Doctor ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be a positive number")]
        public new int ID { get; set; }

        public UpdateDoctorDTO() : base()
        {
            ID = -1;
        }

        public Doctor ToDoctor()
        {
            var person = new Person(FirstName, MiddleName, LastName, Gender, BirthDate, new Country(CountryID), Phone, new User(Email, ""));
            var employee = new Employee(person, new Department(DepartmentID),
                Salary, HireDate, Employee.ToBitmask(WorkingDays), ShiftStart, ShiftEnd, null);
            return new Doctor(employee,
                 CostPerAppointment,
                Specialization
            );
        }
    }
}