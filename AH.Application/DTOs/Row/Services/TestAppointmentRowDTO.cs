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
        public DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; }

        public TestAppointmentRowDTO(int id, string patientFullName, string testName, DateTime appointmentDateTime, string status)
        {
            ID = id;
            PatientFullName = patientFullName;
            TestName = testName;
            AppointmentDateTime = appointmentDateTime;
            Status = status;
        }

        public TestAppointmentRowDTO()
        {
            ID = -1;
            PatientFullName = string.Empty;
            TestName = string.Empty;
            AppointmentDateTime = DateTime.MinValue;
            Status = string.Empty;
        }
    }
}