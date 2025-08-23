namespace AH.Application.DTOs.Row
{
    public class AppointmentRowDTO
    {
        public int ID { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; }

        public AppointmentRowDTO(int id, string patientFullName, string doctorFullName, DateTime appointmentDateTime, string status)
        {
            ID = id;
            PatientFullName = patientFullName;
            DoctorFullName = doctorFullName;
            AppointmentDateTime = appointmentDateTime;
            Status = status;
        }

        public AppointmentRowDTO()
        {
            ID = -1;
            PatientFullName = string.Empty;
            DoctorFullName = string.Empty;
            AppointmentDateTime = DateTime.MinValue;
            Status = string.Empty;
        }
    }
}