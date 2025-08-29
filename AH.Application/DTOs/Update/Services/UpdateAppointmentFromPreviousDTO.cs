using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateAppointmentFromPreviousDTO
    {
        public int AppointmentID { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string? Notes { get; set; }
        public int CreatedByReceptionistID { get; set; }

        public UpdateAppointmentFromPreviousDTO(int appointmentID, DateTime scheduledDate, string? notes, int createdByReceptionistID)
        {
            AppointmentID = appointmentID;
            ScheduledDate = scheduledDate;
            Notes = notes;
            CreatedByReceptionistID = createdByReceptionistID;
        }
    }
}