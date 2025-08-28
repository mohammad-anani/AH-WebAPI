using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateServiceDTO
    {
        public int PatientID { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? ActualStartingDate { get; set; }
        public string Reason { get; set; }
        public string? Result { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
        public int BillAmount { get; set; }
        public int CreatedByReceptionistID { get; set; }

        public CreateServiceDTO()
        {
            PatientID = -1;
            ScheduledDate = DateTime.MinValue;
            ActualStartingDate = null;
            Reason = string.Empty;
            Result = null;
            ResultDate = null;
            Status = string.Empty;
            Notes = null;
            BillAmount = 0;
            CreatedByReceptionistID = -1;
        }

        public CreateServiceDTO(int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
        {
            PatientID = patientID;
            ScheduledDate = scheduledDate;
            ActualStartingDate = actualStartingDate;
            Reason = reason;
            Result = result;
            ResultDate = resultDate;
            Status = status;
            Notes = notes;
            BillAmount = billAmount;
            CreatedByReceptionistID = createdByReceptionistID;
        }

        public Service ToService()
        {
            return new Service(
                new Patient(PatientID),
                ScheduledDate,
                ActualStartingDate,
                Reason,
                Result,
                ResultDate,
                Status,
                Notes,
                new Bill(-1, BillAmount, 0),
                new Receptionist(CreatedByReceptionistID)
            );
        }
    }
}