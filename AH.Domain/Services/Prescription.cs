using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Prescription
    {
        public int ID { get; set; }
        public Appointment Appointment { get; set; }
        public string Diagnosis { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateTime MedicationStart { get; set; }
        public DateTime MedicationEnd { get; set; }
        public string Notes { get; set; }

        public Prescription()
        {
            ID = -1;
            Appointment = new Appointment();
            Diagnosis = "";
            Medication = "";
            Dosage = "";
            Frequency = "";
            MedicationStart = DateTime.MinValue;
            MedicationEnd = DateTime.MinValue;
            Notes = "";
        }

        public Prescription(int id, Appointment appointment, string diagnosis, string medication, string dosage, string frequency, DateTime medicationStart, DateTime medicationEnd, string notes)
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

        public Prescription(Appointment appointment, string diagnosis, string medication, string dosage, string frequency, DateTime medicationStart, DateTime medicationEnd, string notes)
        {
            ID = -1;
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
