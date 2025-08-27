using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateTestAppointmentFromTestOrderDTO
    {
        public int TestOrderID { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string? Notes { get; set; }
        public int CreatedByReceptionistID { get; set; }

        public CreateTestAppointmentFromTestOrderDTO(int testOrderID, DateTime scheduledDate, string? notes, int createdByReceptionistID)
        {
            TestOrderID = testOrderID;
            ScheduledDate = scheduledDate;
            Notes = notes;
            CreatedByReceptionistID = createdByReceptionistID;
        }
    }
}