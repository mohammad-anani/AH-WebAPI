using AH.Application.DTOs.Form;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreatePrescriptionDTO : PrescriptionFormDTO
    {
        [Required(ErrorMessage = "Appointment ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Appointment ID must be a positive number")]
        public int AppointmentID { get; set; }

        public CreatePrescriptionDTO()
        {
            AppointmentID = -1;
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