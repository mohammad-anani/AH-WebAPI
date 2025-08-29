using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateServiceDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Reason must be at least 10 characters")]
        public string Reason { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Notes can be empty or any length")]
        public string? Notes { get; set; }

        public UpdateServiceDTO()
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

        public UpdateServiceDTO(int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
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