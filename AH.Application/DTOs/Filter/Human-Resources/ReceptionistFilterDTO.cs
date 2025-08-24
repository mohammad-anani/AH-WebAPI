using AH.Application.DTOs.Filter.Helpers;

namespace AH.Application.DTOs.Filter
{
    public class ReceptionistFilterDTO : EmployeeFilter, IFilterable
    {
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public ReceptionistFilterDTO(
            string? sort,
            bool? order,
            int? page,
            int? departmentID = null,
            decimal? salaryFrom = null,
            decimal? salaryTo = null,
            DateTime? hireDateFrom = null,
            DateTime? hireDateTo = null,
            DateTime? leaveDateFrom = null,
            DateTime? leaveDateTo = null,
            TimeSpan? shiftStartFrom = null,
            TimeSpan? shiftStartTo = null,
            TimeSpan? shiftEndFrom = null,
            TimeSpan? shiftEndTo = null,
            int? workingDays = null,
            DateTime? createdAtFrom = null,
            DateTime? createdAtTo = null,
            int? createdByAdminID = null,
            string? firstName = null,
            string? middleName = null,
            string? lastName = null,
            char? gender = null,
            DateTime? birthDateFrom = null,
            DateTime? birthDateTo = null,
            int? countryID = null,
            string? phone = null,
            string? email = null)
            : base(departmentID, salaryFrom, salaryTo, hireDateFrom, hireDateTo, leaveDateFrom, leaveDateTo,
                   shiftStartFrom, shiftStartTo, shiftEndFrom, shiftEndTo, workingDays, createdAtFrom, createdAtTo, createdByAdminID)
        {
            Sort = sort;
            Order = order;
            Page = page;
            // Set PersonFilter properties
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

        // Parameterless constructor
        public ReceptionistFilterDTO() : base()
        {
            Sort = null;
            Order = null;
            Page = null;
        }
    }
}