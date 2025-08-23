namespace AH.Application.DTOs.Row
{
    public class PrescriptionRowDTO
    {
        public int ID { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public string MedicationName { get; set; }
        public DateTime PrescribedDate { get; set; }

        public PrescriptionRowDTO(int id, string patientFullName, string doctorFullName, string medicationName, DateTime prescribedDate)
        {
            ID = id;
            PatientFullName = patientFullName;
            DoctorFullName = doctorFullName;
            MedicationName = medicationName;
            PrescribedDate = prescribedDate;
        }

        public PrescriptionRowDTO()
        {
            ID = -1;
            PatientFullName = string.Empty;
            DoctorFullName = string.Empty;
            MedicationName = string.Empty;
            PrescribedDate = DateTime.MinValue;
        }
    }
}