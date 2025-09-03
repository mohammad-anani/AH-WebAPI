using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class InsuranceFormDTO
    {
        [Required(ErrorMessage = "Provider name is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Provider name must be between 10 and 50 characters")]
        public string ProviderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Coverage is required")]
        [Range(0.0, 1.0, ErrorMessage = "Coverage must be between 0 and 1")]
        public decimal Coverage { get; set; }
    }
}
