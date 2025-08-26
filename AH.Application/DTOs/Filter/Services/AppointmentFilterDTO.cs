using AH.Application.DTOs.Filter.Helpers;

namespace AH.Application.DTOs.Filter
{
    public class AppointmentFilterDTO : ServiceFilter, IFilterable
    {
        public int? PreviousAppointmentID { get; set; }
        public int? DoctorID { get; set; }
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public AppointmentFilterDTO(
            int? previousAppointmentID,
            int? doctorID,
            string? sort,
            bool? order,
            int? page,
            int? patientID = null,
            DateTime? scheduledDateFrom = null,
            DateTime? scheduledDateTo = null,
            DateTime? actualStartingDateFrom = null,
            DateTime? actualStartingDateTo = null,
            string? reason = null,
            string? result = null,
            DateTime? resultDateFrom = null,
            DateTime? resultDateTo = null,
            byte? status = null,
            string? notes = null,
            int? amountFrom = null,
            int? amountTo = null,
            int? amountPaidFrom = null,
            int? amountPaidTo = null,
            int? createdByReceptionistID = null,
            DateTime? createdAtFrom = null,
            DateTime? createdAtTo = null)
            : base(patientID, scheduledDateFrom, scheduledDateTo, actualStartingDateFrom, actualStartingDateTo,
                   reason, result, resultDateFrom, resultDateTo, status, notes, amountFrom, amountTo, 
                   amountPaidFrom, amountPaidTo, createdByReceptionistID, createdAtFrom, createdAtTo)
        {
            PreviousAppointmentID = previousAppointmentID;
            DoctorID = doctorID;
            Sort = sort;
            Order = order;
            Page = page;
        }

        // Parameterless constructor
        public AppointmentFilterDTO() : base()
        {
            PreviousAppointmentID = null;
            DoctorID = null;
            Sort = null;
            Order = null;
            Page = null;
        }
    }
}