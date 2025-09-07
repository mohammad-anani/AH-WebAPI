using AH.Application.DTOs.Form;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateServiceDTO : ServiceFormDTO
    {
        [Required(ErrorMessage = "Patient ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be a positive number")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "Scheduled date is required")]
        [FutureDateWithinYear]
        public DateTime ScheduledDate { get; set; }

        //[Required(ErrorMessage = "Bill amount is required")]
        //[Range(10, 99999, ErrorMessage = "Bill amount must be between 10 and 99,999")]
        //public int BillAmount { get; set; }

        [BindNever]
        [Required(ErrorMessage = "Created by receptionist ID is required")]
        public int CreatedByReceptionistID { get; set; }

        public CreateServiceDTO()
        {
            PatientID = -1;
            ScheduledDate = DateTime.MinValue;
            CreatedByReceptionistID = -1;
        }

        public CreateServiceDTO(int patientID, DateTime scheduledDate, string reason, string? notes, int billAmount, int createdByReceptionistID)
        {
            PatientID = patientID;
            ScheduledDate = scheduledDate;
            Reason = reason;
            Notes = notes;
            CreatedByReceptionistID = createdByReceptionistID;
        }

        public Service ToService(string status)
        {
            return new Service(
                new Patient(PatientID),
                ScheduledDate,
                null,
                Reason,
                null,
                null, status,

                Notes,
                new Bill(),
                new Receptionist(CreatedByReceptionistID)
            );
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
                new Bill(),
                new Receptionist(CreatedByReceptionistID)
            );
        }
    }
}