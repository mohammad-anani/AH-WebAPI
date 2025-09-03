using AH.Application.DTOs.Form;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreatePersonDTO : PersonFormDTO
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 10 and 64 characters")]
        public string Password { get; set; }

        public CreatePersonDTO()
        {
            Password = "";
        }

        public CreatePersonDTO(string firstName, string middleName, string lastName, char gender, DateTime birthDate, int countryId, string phone, string email, string password)
        {
            Password = password;
        }

        public Person ToPerson()
        {
            return new Person(FirstName, MiddleName, LastName, Gender, BirthDate, new Country(CountryID), Phone, new User(Email, Password));
        }

        public static string HashPassword(string password)
        {
            User user = new User { Password = password };
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, user.Password);
        }
    }
}