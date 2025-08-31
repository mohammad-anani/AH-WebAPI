using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs
{
    public class SigninRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Email must be between 6 and 40 characters")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public string Password { get; set; }

        public SigninRequestDTO()
        {
            Email = "";
            Password = "";
        }
    }
}