namespace AH.Application.DTOs.Row
{
    public class PrescriptionRowDTO
    {
        public int ID { get; set; }
        public int AppointmentID { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public bool IsOnMedication { get; set; }

        public PrescriptionRowDTO(int id, int appointmentID, string medication, string dosage, string frequency, bool isOnMedication)
        {
            ID = id;
            AppointmentID = appointmentID;
            Medication = medication;
            Dosage = dosage;
            Frequency = frequency;
            IsOnMedication = isOnMedication;
        }

        public PrescriptionRowDTO()
        {
            ID = -1;
            AppointmentID = -1;
            Medication = string.Empty;
            Dosage = string.Empty;
            Frequency = string.Empty;
            IsOnMedication = false;
        }
    }
}