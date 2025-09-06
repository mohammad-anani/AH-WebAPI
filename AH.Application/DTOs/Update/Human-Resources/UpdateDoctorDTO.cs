using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateDoctorDTO : UpdateEmployeeDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Doctor ID is required")]
        public new int ID { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Specialization must be between 5 and 100 characters")]
        public string Specialization { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cost per appointment is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Cost per appointment must be a positive number")]
        public int CostPerAppointment { get; set; }

        public UpdateDoctorDTO() : base()
        {
            ID = -1;
        }

        public Doctor ToDoctor()
        {
            var person = new Person(FirstName, MiddleName, LastName, Gender, BirthDate, new Country(CountryID), Phone, new User(Email, ""));
            var employee = new Employee(person, new Department(DepartmentID),
                Salary, HireDate, Employee.ToBitmask(WorkingDays), ShiftStart, ShiftEnd, null);
            return new Doctor(ID, employee,
                 CostPerAppointment,
                Specialization
            );
        }
    }
}