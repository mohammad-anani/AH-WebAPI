using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateDoctorDTO : UpdateEmployeeDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Specialization must be between 5 and 100 characters")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Cost per appointment is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Cost per appointment must be a positive number")]
        public int CostPerAppointment { get; set; }

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