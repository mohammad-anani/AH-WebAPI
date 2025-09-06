using AH.Application.DTOs.Form;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdatePrescriptionDTO : PrescriptionFormDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Prescription ID is required")]
        public int ID { get; set; }

        public UpdatePrescriptionDTO()
        {
            ID = -1;
        }

        public UpdatePrescriptionDTO(int appointmentID, string diagnosis, string medication, string dosage, string frequency, DateTime medicationStart, DateTime medicationEnd, string notes)
        {
        }

        public Prescription ToPrescription()
        {
            return new Prescription(ID,
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