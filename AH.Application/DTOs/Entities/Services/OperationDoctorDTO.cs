using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Entities.Services
{
    public class OperationDoctorDTO
    {
        [Required(ErrorMessage = "Doctor ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be a positive number")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Role must be between 10 and 50 characters")]
        public string Role { get; set; }

        public OperationDoctorDTO(int doctorID, string role)
        {
            DoctorID = doctorID;
            Role = role;
        }
    }
}