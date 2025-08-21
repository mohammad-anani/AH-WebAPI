using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Country Country { get; set; }
        public string Phone { get; set; }
        public User User { get; set; }

        public Person()
        {
            ID = -1;
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Gender = '\0';
            BirthDate = DateTime.MinValue;
            Country = new Country();
            Phone = "";
            User = new User();
        }

        public Person(int id, string firstName, string middleName, string lastName, char gender, DateTime birthDate, Country country, string phone, User user)
        {
            ID = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            Country = country;
            Phone = phone;
            User = user;
        }

        public Person(string firstName, string middleName, string lastName, char gender, DateTime birthDate, Country country, string phone, User user)
        {
            ID = -1;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            Country = country;
            Phone = phone;
            User = user;
        }
    }
}
