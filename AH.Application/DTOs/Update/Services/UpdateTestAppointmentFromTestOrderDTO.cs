using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateTestAppointmentFromTestOrderDTO
    {
        public int TestOrderID { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string? Notes { get; set; }
        public int CreatedByReceptionistID { get; set; }

        public UpdateTestAppointmentFromTestOrderDTO(int testOrderID, DateTime scheduledDate, string? notes, int createdByReceptionistID)
        {
            TestOrderID = testOrderID;
            ScheduledDate = scheduledDate;
            Notes = notes;
            CreatedByReceptionistID = createdByReceptionistID;
        }
    }
}