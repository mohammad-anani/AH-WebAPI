using AH.Application.DTOs.Update.Validation;
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
        [Required(ErrorMessage = "Patient ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be a positive number")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "Scheduled date is required")]
        [FutureDateWithinYear]
        public DateTime ScheduledDate { get; set; }

        public DateTime? ActualStartingDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Reason must be at least 10 characters")]
        public string Reason { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Result must be at least 10 characters")]
        public string? Result { get; set; }

        public DateTime? ResultDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Range(1, 6, ErrorMessage = "Status must be between 1 and 6")]
        public string Status { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Notes can be empty or any length")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Bill amount is required")]
        [Range(10, 99999, ErrorMessage = "Bill amount must be between 10 and 99,999")]
        public int BillAmount { get; set; }

        [Required(ErrorMessage = "Created by receptionist ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Created by receptionist ID must be a positive number")]
        public int CreatedByReceptionistID { get; set; }

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