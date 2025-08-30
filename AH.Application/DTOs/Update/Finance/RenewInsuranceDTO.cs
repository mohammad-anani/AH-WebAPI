using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class RenewInsuranceDTO
    {
        [BindNever]
        public int ID { get; set; }

        [Required(ErrorMessage = "Expiration date is required")]
        [FutureDateWithinYear]
        public DateOnly ExpirationDate { get; set; }

        [Required(ErrorMessage = "Coverage is required")]
        [Range(0.0, 1.0, ErrorMessage = "Coverage must be between 0 and 1")]
        public decimal Coverage { get; set; }

        public RenewInsuranceDTO()
        {
            ID = -1;
            Coverage = 0;
            ExpirationDate = DateOnly.MinValue;
        }

        public RenewInsuranceDTO(decimal coverage, DateOnly expirationDate)
        {
            ExpirationDate = expirationDate;
            Coverage = coverage;
        }

        public Insurance ToInsurance()
        {
            return new Insurance(
                -1,
                new Patient(-1),
                "",
                Coverage,
                ExpirationDate,
                true,
                DateTime.MinValue,
                new Receptionist(-1)
            );
        }
    }
}