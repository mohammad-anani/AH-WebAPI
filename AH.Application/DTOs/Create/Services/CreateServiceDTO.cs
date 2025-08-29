using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateServiceDTO
    {
        [Required(ErrorMessage = "Patient ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be a positive number")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "Scheduled date is required")]
        [FutureDateWithinYear]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Reason must be at least 10 characters")]
        public string Reason { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Notes can be empty or any length")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Bill amount is required")]
        [Range(10, 99999, ErrorMessage = "Bill amount must be between 10 and 99,999")]
        public int BillAmount { get; set; }

        [Required(ErrorMessage = "Created by receptionist ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Created by receptionist ID must be a positive number")]
        public int CreatedByReceptionistID { get; set; }

        public CreateServiceDTO()
        {
            PatientID = -1;
            ScheduledDate = DateTime.MinValue;
            Reason = string.Empty;
            Notes = null;
            BillAmount = 0;
            CreatedByReceptionistID = -1;
        }

        public CreateServiceDTO(int patientID, DateTime scheduledDate, string reason, string? notes, int billAmount, int createdByReceptionistID)
        {
            PatientID = patientID;
            ScheduledDate = scheduledDate;
            Reason = reason;
            Notes = notes;
            BillAmount = billAmount;
            CreatedByReceptionistID = createdByReceptionistID;
        }

        public Service ToService()
        {
            return new Service(
                new Patient(PatientID),
                ScheduledDate,
                null,
                Reason,
                null,
                null, "",

                Notes,
                new Bill(-1, BillAmount, 0),
                new Receptionist(CreatedByReceptionistID)
            );
        }
    }
}