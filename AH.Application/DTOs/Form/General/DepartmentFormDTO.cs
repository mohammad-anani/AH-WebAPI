using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class DepartmentFormDTO
    {
        [Required(ErrorMessage = "Department name is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Department name must be between 5 and 20 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Phone must be exactly 8 digits")]
        public string Phone { get; set; } = string.Empty;
    }
}
