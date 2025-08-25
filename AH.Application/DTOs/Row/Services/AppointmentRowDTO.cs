namespace AH.Application.DTOs.Row
{
    public class AppointmentRowDTO
    {
        public int ID { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }

        public bool IsFollowUp { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; }

        public bool IsPaid { get; set; }

        public AppointmentRowDTO(int id, string patientFullName, string doctorFullName, bool isFollowUp, DateTime scheduledDate, string status, bool isPaid)
        {
            ID = id;
            PatientFullName = patientFullName;
            DoctorFullName = doctorFullName;
            IsFollowUp = isFollowUp;
            ScheduledDate = scheduledDate;
            Status = status;
            IsPaid = isPaid;
        }

        public AppointmentRowDTO()
        {
            ID = -1;
            PatientFullName = string.Empty;
            DoctorFullName = string.Empty;
            IsFollowUp = false;
            ScheduledDate = DateTime.MinValue;
            Status = string.Empty;
            IsPaid = false;
        }
    }
}