using AH.Application.DTOs.Entities.Services;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class OperationFormDTO : ServiceFormDTO
    {
        [Required(ErrorMessage = "Operation name is required")]
        [Range(10, 100, ErrorMessage = "Operation name must be between 10 and 100")]
        public int OperationName { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Description must be at least 10 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Operation doctors list is required")]
        [MinLength(1, ErrorMessage = "At least 1 doctor is required")]
        [MaxLength(5, ErrorMessage = "Maximum 5 doctors allowed")]
        public List<OperationDoctorDTO> OperationDoctors { get; set; } = new();
    }
}
