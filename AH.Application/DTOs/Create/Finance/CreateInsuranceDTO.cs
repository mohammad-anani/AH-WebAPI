using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateInsuranceDTO
    {
        [Required(ErrorMessage = "Patient ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be a positive number")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "Provider name is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Provider name must be between 10 and 50 characters")]
        public string ProviderName { get; set; }

        [Required(ErrorMessage = "Coverage is required")]
        [Range(0.0, 1.0, ErrorMessage = "Coverage must be between 0 and 1")]
        public decimal Coverage { get; set; }

        [Required(ErrorMessage = "Expiration date is required")]
        [FutureDateWithinYear]
        public DateOnly ExpirationDate { get; set; }

        [BindNever]
        public int CreatedByReceptionistID { get; set; }

        public CreateInsuranceDTO()
        {
            PatientID = -1;
            ProviderName = string.Empty;
            Coverage = 0;
            ExpirationDate = DateOnly.MinValue;
            CreatedByReceptionistID = -1;
        }

        public CreateInsuranceDTO(int patientID, string providerName, decimal coverage, DateOnly expirationDate, int createdByReceptionistID)
        {
            PatientID = patientID;
            ProviderName = providerName;
            Coverage = coverage;
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