using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateInsuranceDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Provider name is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Provider name must be between 10 and 50 characters")]
        public string ProviderName { get; set; }

        [Required(ErrorMessage = "Coverage is required")]
        [Range(0.0, 1.0, ErrorMessage = "Coverage must be between 0 and 1")]
        public decimal Coverage { get; set; }

        public UpdateInsuranceDTO()
        {
            PatientID = -1;
            ProviderName = string.Empty;
            Coverage = 0;
            ExpirationDate = DateOnly.MinValue;
            IsActive = true;
            CreatedByReceptionistID = -1;
        }

        public UpdateInsuranceDTO(int patientID, string providerName, decimal coverage, DateOnly expirationDate, bool isActive, int createdByReceptionistID)
        {
            PatientID = patientID;
            ProviderName = providerName;
            Coverage = coverage;
            ExpirationDate = expirationDate;
            IsActive = isActive;
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
                IsActive,
                DateTime.MinValue,
                new Receptionist(CreatedByReceptionistID)
            );
        }
    }
}