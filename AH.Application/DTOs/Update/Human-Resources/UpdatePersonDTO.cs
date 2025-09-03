using AH.Application.DTOs.Form;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdatePersonDTO : PersonFormDTO
    {
        public UpdatePersonDTO()
        {
        }

        public UpdatePersonDTO(string firstName, string middleName, string lastName, char gender, DateTime birthDate, int countryId, string phone, string email, string password)
        {
        }

        public Person ToPerson()
        {
            return new Person(FirstName, MiddleName, LastName, Gender, BirthDate, new Country(CountryID), Phone, new User(Email, ""));
        }
    }
}