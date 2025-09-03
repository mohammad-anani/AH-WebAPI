using AH.Application.DTOs.Form;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateInsuranceDTO : InsuranceFormDTO
    {
        [Required(ErrorMessage = "Patient ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be a positive number")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "Expiration date is required")]
        [FutureDateWithinYear]
        public DateOnly ExpirationDate { get; set; }

        [BindNever]
        public int CreatedByReceptionistID { get; set; }

        public CreateInsuranceDTO()
        {
            PatientID = -1;
            ExpirationDate = DateOnly.MinValue;
            CreatedByReceptionistID = -1;
        }

        public CreateInsuranceDTO(int patientID, string providerName, decimal coverage, DateOnly expirationDate, int createdByReceptionistID)
        {
            PatientID = patientID;
            ExpirationDate = expirationDate;
            CreatedByReceptionistID = createdByReceptionistID;
        }

        public Insurance ToInsurance()
        {
            return new Insurance(
                -1,
                new Patient(PatientID),
                ProviderName,
                Coverage,
                ExpirationDate,
                true,
                DateTime.MinValue,
                new Receptionist(CreatedByReceptionistID)
            );
        }
    }
}