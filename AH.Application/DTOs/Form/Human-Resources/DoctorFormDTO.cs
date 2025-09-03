using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class DoctorFormDTO : EmployeeFormDTO
    {
        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Specialization must be between 5 and 100 characters")]
        public string Specialization { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cost per appointment is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Cost per appointment must be a positive number")]
        public int CostPerAppointment { get; set; }
    }
}
