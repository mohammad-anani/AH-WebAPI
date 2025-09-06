using AH.Application.DTOs.Validation;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class PersonFormDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 20 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Middle name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Middle name must be between 3 and 20 characters")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 20 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("^[MF]$", ErrorMessage = "Gender must be 'M' or 'F'")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [PastDateWithin120Years]
        public DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "Country ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Country ID must be a positive number")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Phone must be exactly 8 digits")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Email must be between 6 and 40 characters")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }
}
