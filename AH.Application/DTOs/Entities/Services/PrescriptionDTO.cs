using AH.Application.DTOs.Row;

namespace AH.Application.DTOs.Entities
{
    public class PrescriptionDTO
    {
        public int ID { get; set; }
        public AppointmentRowDTO Appointment { get; set; }
        public string Diagnosis { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateTime MedicationStart { get; set; }
        public DateTime MedicationEnd { get; set; }
        public string Notes { get; set; }

        public PrescriptionDTO()
        {
            ID = -1;
            Appointment = new AppointmentRowDTO();
            Diagnosis = string.Empty;
            Medication = string.Empty;
            Dosage = string.Empty;
            Frequency = string.Empty;
            MedicationStart = DateTime.MinValue;
            MedicationEnd = DateTime.MinValue;
            Notes = string.Empty;
        }

        public PrescriptionDTO(int id, AppointmentRowDTO appointment, string diagnosis, string medication, string dosage, string frequency, DateTime medicationStart, DateTime medicationEnd, string notes)
        {
            ID = id;
            Appointment = appointment;
            Diagnosis = diagnosis;
            Medication = medication;
            Dosage = dosage;
            Frequency = frequency;
            MedicationStart = medicationStart;
            MedicationEnd = medicationEnd;
            Notes = notes;
        }
    }
}