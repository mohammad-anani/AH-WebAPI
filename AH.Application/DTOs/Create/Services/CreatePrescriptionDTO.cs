using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreatePrescriptionDTO
    {
        public int AppointmentID { get; set; }
        public string Diagnosis { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateTime MedicationStart { get; set; }
        public DateTime MedicationEnd { get; set; }
        public string Notes { get; set; }

        public CreatePrescriptionDTO()
        {
            AppointmentID = -1;
            Diagnosis = string.Empty;
            Medication = string.Empty;
            Dosage = string.Empty;
            Frequency = string.Empty;
            MedicationStart = DateTime.MinValue;
            MedicationEnd = DateTime.MinValue;
            Notes = string.Empty;
        }

        public CreatePrescriptionDTO(int appointmentID, string diagnosis, string medication, string dosage, string frequency, DateTime medicationStart, DateTime medicationEnd, string notes)
        {
            AppointmentID = appointmentID;
            Diagnosis = diagnosis;
            Medication = medication;
            Dosage = dosage;
            Frequency = frequency;
            MedicationStart = medicationStart;
            MedicationEnd = medicationEnd;
            Notes = notes;
        }

        public Prescription ToPrescription()
        {
            return new Prescription(
                new Appointment(AppointmentID),
                Diagnosis,
                Medication,
                Dosage,
                Frequency,
                MedicationStart,
                MedicationEnd,
                Notes
            );
        }
    }
}