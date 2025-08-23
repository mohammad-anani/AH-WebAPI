namespace AH.Application.DTOs.Filter
{
    public class DoctorFilterDTO : EmployeeFilter, IFilterable
    {
        public string? Specialization { get; set; }

        public int? CostPerAppointmentFrom { get; set; }

        public int? CostPerAppointmentTo { get; set; }

        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public DoctorFilterDTO(
            string? specialization,
            int? costPerAppointmentFrom,
            int? costPerAppointmentTo,
            string? sort,
            bool? order,
            int? page,
            int? departmentId = null,
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
            int? countryId = null,
            string? phone = null,
            string? email = null)
            : base(departmentId, salaryFrom, salaryTo, hireDateFrom, hireDateTo, leaveDateFrom, leaveDateTo,
                   shiftStartFrom, shiftStartTo, shiftEndFrom, shiftEndTo, workingDays, createdAtFrom, createdAtTo, createdByAdminID)
        {
            Specialization = specialization;
            CostPerAppointmentFrom = costPerAppointmentFrom;
            CostPerAppointmentTo = costPerAppointmentTo;
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
            CountryId = countryId;
            Phone = phone;
            Email = email;
        }

        // Parameterless constructor
        public DoctorFilterDTO() : base()
        {
            Specialization = null;
            CostPerAppointmentFrom = null;
            CostPerAppointmentTo = null;
            Sort = null;
            Order = null;
            Page = null;
        }
    }
}