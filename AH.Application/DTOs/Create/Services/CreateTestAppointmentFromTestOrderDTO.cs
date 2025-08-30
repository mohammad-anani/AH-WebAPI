using AH.Application.DTOs.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateTestAppointmentFromTestOrderDTO
    {
        [Required(ErrorMessage = "Test order ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Test order ID must be a positive number")]
        [BindNever]
        public int TestOrderID { get; set; }

        [Required(ErrorMessage = "Scheduled date is required")]
        [FutureDateWithinYear]
        public DateTime ScheduledDate { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Notes can be empty or any length")]
        public string? Notes { get; set; }

        [BindNever]
        public int CreatedByReceptionistID { get; set; }

        public CreateTestAppointmentFromTestOrderDTO()
        {
            TestOrderID = -1;
            ScheduledDate = DateTime.MinValue;
            Notes = null;
            CreatedByReceptionistID = -1;
        }

        public CreateTestAppointmentFromTestOrderDTO(int testOrderID, DateTime scheduledDate, string? notes, int createdByReceptionistID)
        {
            TestOrderID = testOrderID;
            ScheduledDate = scheduledDate;
            Notes = notes;
            CreatedByReceptionistID = createdByReceptionistID;
        }
    }
}