using AH.Application.DTOs.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateAppointmentFromPreviousDTO
    {
        [Required(ErrorMessage = "Appointment ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Appointment ID must be a positive number")]
        public int AppointmentID { get; set; }

        [Required(ErrorMessage = "Scheduled date is required")]
        [FutureDateWithinYear]
        public DateTime ScheduledDate { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Notes can be empty or any length")]
        public string? Notes { get; set; }

        [BindNever]
        public int CreatedByReceptionistID { get; set; }

        public CreateAppointmentFromPreviousDTO()
        {
            AppointmentID = -1;
            ScheduledDate = DateTime.MinValue;
            Notes = null;
            CreatedByReceptionistID = -1;
        }

        public CreateAppointmentFromPreviousDTO(int appointmentID, DateTime scheduledDate, string? notes, int createdByReceptionistID)
        {
            AppointmentID = appointmentID;
            ScheduledDate = scheduledDate;
            Notes = notes;
            CreatedByReceptionistID = createdByReceptionistID;
        }
    }
}