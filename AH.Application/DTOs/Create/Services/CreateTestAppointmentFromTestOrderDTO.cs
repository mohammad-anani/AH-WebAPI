using AH.Application.DTOs.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateTestAppointmentFromTestOrderDTO
    {
        [BindNever]
        public int TestOrderID { get; set; }

        [Required(ErrorMessage = "Scheduled date is required")]
        [FutureDateWithinYear]
        public DateTime ScheduledDate { get; set; }

 

        [BindNever]
        public int CreatedByReceptionistID { get; set; }

        public CreateTestAppointmentFromTestOrderDTO()
        {
            TestOrderID = -1;
            ScheduledDate = DateTime.MinValue;
            CreatedByReceptionistID = -1;
        }

        public CreateTestAppointmentFromTestOrderDTO(int testOrderID, DateTime scheduledDate, int createdByReceptionistID)
        {
            TestOrderID = testOrderID;
            ScheduledDate = scheduledDate;
            CreatedByReceptionistID = createdByReceptionistID;
        }
    }
}