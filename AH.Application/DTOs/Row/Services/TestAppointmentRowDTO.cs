using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Row
{
    public class TestAppointmentRowDTO
    {
        public int ID { get; set; }
        public string PatientFullName { get; set; }
        public string TestName { get; set; }
        public bool IsOrdered { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; }
        public bool IsPaid { get; set; }

        // Empty constructor with defaults
        public TestAppointmentRowDTO()
        {
            ID = 0;
            PatientFullName = string.Empty;
            TestName = string.Empty;
            IsOrdered = false;
            ScheduledDate = DateTime.MinValue;
            Status = string.Empty;
            IsPaid = false;
        }

        // Full constructor
        public TestAppointmentRowDTO(int id, string patientFullName, string testName, bool isOrdered, DateTime scheduledDate, string status, bool isPaid)
        {
            ID = id;
            PatientFullName = patientFullName;
            TestName = testName;
            IsOrdered = isOrdered;
            ScheduledDate = scheduledDate;
            Status = status;
            IsPaid = isPaid;
        }
    }
}