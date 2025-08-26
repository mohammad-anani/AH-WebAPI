using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.DTOs.Entities
{
    public class ServiceDTO
    {
        public PatientRowDTO Patient { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? ActualStartingDate { get; set; }
        public string Reason { get; set; }
        public string? Result { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
        public Bill Bill { get; set; }
        public ReceptionistRowDTO CreatedByReceptionist { get; set; }
        public DateTime CreatedAt { get; set; }

        public ServiceDTO()
        {
            Patient = new PatientRowDTO();
            ScheduledDate = DateTime.MinValue;
            ActualStartingDate = null;
            Reason = string.Empty;
            Result = null;
            ResultDate = null;
            Status = string.Empty;
            Notes = null;
            Bill = new Bill();
            CreatedByReceptionist = new ReceptionistRowDTO();
            CreatedAt = DateTime.MinValue;
        }

        public ServiceDTO(PatientRowDTO patient, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, Bill bill, ReceptionistRowDTO createdByReceptionist, DateTime createdAt)
        {
            Patient = patient;
            ScheduledDate = scheduledDate;
            ActualStartingDate = actualStartingDate;
            Reason = reason;
            Result = result;
            ResultDate = resultDate;
            Status = status;
            Notes = notes;
            Bill = bill;
            CreatedByReceptionist = createdByReceptionist;
            CreatedAt = createdAt;
        }
    }
}