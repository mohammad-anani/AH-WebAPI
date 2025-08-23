namespace AH.Domain.Entities
{
    public class Person
    {
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
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Gender = '\0';
            BirthDate = DateTime.MinValue;
            Country = new Country();
            Phone = "";
            User = new User();
        }

        public Person(string firstName, string middleName, string lastName, char gender, DateTime birthDate, Country country, string phone, User user)
        {
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