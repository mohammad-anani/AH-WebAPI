using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdatePrescriptionDTO
    {
        [Required(ErrorMessage = "Prescription ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Prescription ID must be a positive number")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Diagnosis is required")]
        [StringLength(256, MinimumLength = 10, ErrorMessage = "Diagnosis must be between 10 and 256 characters")]
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "Medication is required")]
        [StringLength(256, MinimumLength = 5, ErrorMessage = "Medication must be between 5 and 256 characters")]
        public string Medication { get; set; }

        [Required(ErrorMessage = "Dosage is required")]
        [StringLength(256, MinimumLength = 5, ErrorMessage = "Dosage must be between 5 and 256 characters")]
        public string Dosage { get; set; }

        [Required(ErrorMessage = "Frequency is required")]
        [StringLength(256, MinimumLength = 5, ErrorMessage = "Frequency must be between 5 and 256 characters")]
        public string Frequency { get; set; }

        [Required(ErrorMessage = "Medication start date is required")]
        [MedicationDateRange("MedicationEnd")]
        public DateTime MedicationStart { get; set; }

        [Required(ErrorMessage = "Medication end date is required")]
        public DateTime MedicationEnd { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Notes can be empty or any length")]
        public string Notes { get; set; }

        public UpdatePrescriptionDTO()
        {
            ID = -1;
            Diagnosis = string.Empty;
            Medication = string.Empty;
            Dosage = string.Empty;
            Frequency = string.Empty;
            MedicationStart = DateTime.MinValue;
            MedicationEnd = DateTime.MinValue;
            Notes = string.Empty;
        }

        public UpdatePrescriptionDTO(int appointmentID, string diagnosis, string medication, string dosage, string frequency, DateTime medicationStart, DateTime medicationEnd, string notes)
        {
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
                new Appointment(-1),
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