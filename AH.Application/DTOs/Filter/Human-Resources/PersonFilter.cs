namespace AH.Application.DTOs.Filter
{
    public class PersonFilter
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public char? Gender { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        public int? CountryID { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        // Full constructor
        public PersonFilter(
            string? firstName,
            string? middleName,
            string? lastName,
            char? gender,
            DateTime? birthDateFrom,
            DateTime? birthDateTo,
            int? countryID,
            string? phone,
            string? email)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDateFrom = birthDateFrom;
            BirthDateTo = birthDateTo;
            CountryID = countryID;
            Phone = phone;
            Email = email;
        }

        // Optional: parameterless constructor
        public PersonFilter()
        { }
    }
}