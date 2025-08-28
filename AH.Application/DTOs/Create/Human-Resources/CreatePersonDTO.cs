using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreatePersonDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int CountryID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public CreatePersonDTO()
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

        public CreatePersonDTO(string firstName, string middleName, string lastName, char gender, DateTime birthDate, int countryId, string phone, string email, string password)
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