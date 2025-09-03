using AH.Application.DTOs.Validation;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class PrescriptionFormDTO
    {
        [Required(ErrorMessage = "Diagnosis is required")]
        [StringLength(256, MinimumLength = 10, ErrorMessage = "Diagnosis must be between 10 and 256 characters")]
        public string Diagnosis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Medication is required")]
        [StringLength(256, MinimumLength = 5, ErrorMessage = "Medication must be between 5 and 256 characters")]
        public string Medication { get; set; } = string.Empty;

        [Required(ErrorMessage = "Dosage is required")]
        [StringLength(256, MinimumLength = 5, ErrorMessage = "Dosage must be between 5 and 256 characters")]
        public string Dosage { get; set; } = string.Empty;

        [Required(ErrorMessage = "Frequency is required")]
        [StringLength(256, MinimumLength = 5, ErrorMessage = "Frequency must be between 5 and 256 characters")]
        public string Frequency { get; set; } = string.Empty;

        [Required(ErrorMessage = "Medication start date is required")]
        [MedicationDateRange("MedicationEnd")]
        public DateTime MedicationStart { get; set; }

        [Required(ErrorMessage = "Medication end date is required")]
        public DateTime MedicationEnd { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Notes can be empty or any length")]
        public string Notes { get; set; } = string.Empty;
    }
}
