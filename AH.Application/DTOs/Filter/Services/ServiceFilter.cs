using AH.Application.DTOs.Filter.Helpers;

namespace AH.Application.DTOs.Filter
{
    public class ServiceFilter : IReceptionistAudit
    {
        public int? PatientID { get; set; }
        public DateTime? ScheduledDateFrom { get; set; }
        public DateTime? ScheduledDateTo { get; set; }
        public DateTime? ActualStartingDateFrom { get; set; }
        public DateTime? ActualStartingDateTo { get; set; }
        public string? Reason { get; set; }
        public string? Result { get; set; }
        public DateTime? ResultDateFrom { get; set; }
        public DateTime? ResultDateTo { get; set; }
        public byte? Status { get; set; }
        public string? Notes { get; set; }
        public int? BillID { get; set; }
        public int? CreatedByReceptionistID { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }

        // Full constructor
        public ServiceFilter(
            int? patientID,
            DateTime? scheduledDateFrom,
            DateTime? scheduledDateTo,
            DateTime? actualStartingDateFrom,
            DateTime? actualStartingDateTo,
            string? reason,
            string? result,
            DateTime? resultDateFrom,
            DateTime? resultDateTo,
            byte? status,
            string? notes,
            int? billID,
            int? createdByReceptionistID,
            DateTime? createdAtFrom,
            DateTime? createdAtTo)
        {
            PatientID = patientID;
            ScheduledDateFrom = scheduledDateFrom;
            ScheduledDateTo = scheduledDateTo;
            ActualStartingDateFrom = actualStartingDateFrom;
            ActualStartingDateTo = actualStartingDateTo;
            Reason = reason;
            Result = result;
            ResultDateFrom = resultDateFrom;
            ResultDateTo = resultDateTo;
            Status = status;
            Notes = notes;
            BillID = billID;
            CreatedByReceptionistID = createdByReceptionistID;
            CreatedAtFrom = createdAtFrom;
            CreatedAtTo = createdAtTo;
        }

        // Parameterless constructor
        public ServiceFilter()
        {
            PatientID = null;
            ScheduledDateFrom = null;
            ScheduledDateTo = null;
            ActualStartingDateFrom = null;
            ActualStartingDateTo = null;
            Reason = null;
            Result = null;
            ResultDateFrom = null;
            ResultDateTo = null;
            Status = null;
            Notes = null;
            BillID = null;
            CreatedByReceptionistID = null;
            CreatedAtFrom = null;
            CreatedAtTo = null;
        }
    }
}