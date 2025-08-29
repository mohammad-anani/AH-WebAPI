using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdatePersonDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Middle name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Middle name must be between 3 and 20 characters")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 20 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("^[MF]$", ErrorMessage = "Gender must be 'M' or 'F'")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [PastDateWithin120Years]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Country ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Country ID must be a positive number")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Phone must be exactly 8 digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Email must be between 6 and 40 characters")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(64, MinimumLength = 10, ErrorMessage = "Password must be between 10 and 64 characters")]
        public string Password { get; set; }

        public UpdatePersonDTO()
        {
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Gender = '\0';
            BirthDate = DateTime.MinValue;
            CountryID = -1;
            Phone = "";
            Email = "";
            Password = "";
        }

        public UpdatePersonDTO(string firstName, string middleName, string lastName, char gender, DateTime birthDate, int countryId, string phone, string email, string password)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            CountryID = countryId;
            Phone = phone;
            Email = email;
            Password = password;
        }

        public Person ToPerson()
        {
            return new Person(FirstName, MiddleName, LastName, Gender, BirthDate, new Country(CountryID), Phone, new User(Email, Password));
        }
    }
}