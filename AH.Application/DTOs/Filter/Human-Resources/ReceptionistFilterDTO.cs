using AH.Application.DTOs.Filter.Helpers;
using AH.Domain.Entities;

namespace AH.Application.DTOs.Filter
{
    public class ReceptionistFilterDTO : EmployeeFilter, IFilterable
    {
        public string? Sort { get; set; }
        public string? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public ReceptionistFilterDTO(
            string? sort,
            string? order,
            int? page,
            int? departmentID = null,
            int? salaryFrom = null,
            int? salaryTo = null,
            DateOnly? hireDateFrom = null,
            DateOnly? hireDateTo = null,
            DateOnly? leaveDateFrom = null,
            DateOnly? leaveDateTo = null,
            TimeSpan? shiftStartFrom = null,
            TimeSpan? shiftStartTo = null,
            TimeSpan? shiftEndFrom = null,
            TimeSpan? shiftEndTo = null,
            string? workingDays = null,
            DateTime? createdAtFrom = null,
            DateTime? createdAtTo = null,
            int? createdByAdminID = null,
            string? firstName = null,
            string? middleName = null,
            string? lastName = null,
            char? gender = null,
            DateOnly? birthDateFrom = null,
            DateOnly? birthDateTo = null,
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