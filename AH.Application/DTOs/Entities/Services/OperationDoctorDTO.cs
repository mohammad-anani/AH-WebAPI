using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Entities.Services
{
    public class OperationDoctorDTO
    {
        [Required(ErrorMessage = "Doctor ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be a positive number")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Role must be between 10 and 50 characters")]
        public string Role { get; set; }

        public OperationDoctorDTO(int doctorID, string role)
        {
            ID = doctorID;
            Role = role;
        }
    }
}