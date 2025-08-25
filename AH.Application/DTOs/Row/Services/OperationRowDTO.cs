namespace AH.Application.DTOs.Row
{
    public class OperationRowDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string PatientFullName { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; }

        public bool IsPaid { get; set; }

        public OperationRowDTO(int iD, string name, string patientFullName, DateTime scheduledDate, string status, bool isPaid)
        {
            ID = iD;
            Name = name;
            PatientFullName = patientFullName;
            ScheduledDate = scheduledDate;
            Status = status;
            IsPaid = isPaid;
        }

        public OperationRowDTO()
        {
            ID = -1;
            Name = string.Empty;
            PatientFullName = string.Empty;
            ScheduledDate = DateTime.MinValue;
            IsPaid = false;
            Status = string.Empty;
        }
    }
}