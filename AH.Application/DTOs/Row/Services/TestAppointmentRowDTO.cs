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

        public TestAppointmentRowDTO(int iD, string patientFullName, string testName, bool isOrdered, DateTime scheduledDate, string status, bool isPaid)
        {
            ID = iD;
            PatientFullName = patientFullName;
            TestName = testName;
            IsOrdered = isOrdered;
            ScheduledDate = scheduledDate;
            Status = status;
            IsPaid = isPaid;
        }

        public TestAppointmentRowDTO()
        {
            ID = -1;
            PatientFullName = String.Empty;
            TestName = String.Empty;
            IsOrdered = false;
            IsPaid = false;
            Status = String.Empty;
        }
    }
}