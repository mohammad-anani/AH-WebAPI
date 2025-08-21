using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class ServiceFilter
    {
        public int? PatientId { get; set; }
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
        public int? BillId { get; set; }
        public int? CreatedByReceptionistId { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }

        // Full constructor
        public ServiceFilter(
            int? patientId,
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
            int? billId,
            int? createdByReceptionistId,
            DateTime? createdAtFrom,
            DateTime? createdAtTo)
        {
            PatientId = patientId;
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
            BillId = billId;
            CreatedByReceptionistId = createdByReceptionistId;
            CreatedAtFrom = createdAtFrom;
            CreatedAtTo = createdAtTo;
        }

        // Parameterless constructor
        public ServiceFilter()
        {
            PatientId = null;
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
            BillId = null;
            CreatedByReceptionistId = null;
            CreatedAtFrom = null;
            CreatedAtTo = null;
        }
    }
}