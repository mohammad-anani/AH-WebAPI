using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateDoctorDTO : UpdateEmployeeDTO
    {
        [Required(ErrorMessage = "Doctor ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be a positive number")]
        public new int ID { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Specialization must be between 5 and 100 characters")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Cost per appointment is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Cost per appointment must be a positive number")]
        public int CostPerAppointment { get; set; }

        public UpdateDoctorDTO() : base()
        {
            ID = -1;
            Specialization = string.Empty;
            CostPerAppointment = 0;
        }

        public Doctor ToDoctor()
        {
            return new Doctor(base.ToEmployee()
            ,
                 CostPerAppointment,
                Specialization
            );
        }
    }
}