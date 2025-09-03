using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class ServiceFormDTO
    {
        [Required(ErrorMessage = "Reason is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Reason must be at least 10 characters")]
        public string Reason { get; set; } = string.Empty;

        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Notes can be empty or any length")]
        public string? Notes { get; set; }
    }
}
