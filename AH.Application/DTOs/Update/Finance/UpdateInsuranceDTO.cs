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
        [Required(ErrorMessage = "Insurance ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Insurance ID must be a positive number")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Provider name is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Provider name must be between 10 and 50 characters")]
        public string ProviderName { get; set; }

        [Required(ErrorMessage = "Coverage is required")]
        [Range(0.0, 1.0, ErrorMessage = "Coverage must be between 0 and 1")]
        public decimal Coverage { get; set; }

        public UpdateInsuranceDTO()
        {
            ID = -1;
            ProviderName = string.Empty;
            Coverage = 0;
        }

        public UpdateInsuranceDTO(int patientID, string providerName, decimal coverage, DateOnly expirationDate, bool isActive, int createdByReceptionistID)
        {
            ProviderName = providerName;
            Coverage = coverage;
        }

        public Insurance ToInsurance()
        {
            return new Insurance(
                -1,
                new Patient(-1),
                ProviderName,
                Coverage,
                DateOnly.MinValue,
                true,
                DateTime.MinValue,
                new Receptionist(-1)
            );
        }
    }
}