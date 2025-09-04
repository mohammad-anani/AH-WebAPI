using AH.Application.DTOs.Form;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

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
            // Deterministic SHA-256 hash to allow DB comparison in stored procedures
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }
    }
}