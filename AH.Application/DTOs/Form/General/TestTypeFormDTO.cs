using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class TestTypeFormDTO
    {
        [Required(ErrorMessage = "Test type name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Test type name must be between 3 and 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        [Range(10, 999, ErrorMessage = "Cost must be between 10 and 999")]
        public int Cost { get; set; }
    }
}
