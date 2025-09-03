using AH.Application.DTOs.Form;
using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateInsuranceDTO : InsuranceFormDTO
    {
        [BindNever]
        public int ID { get; set; }

        public UpdateInsuranceDTO()
        {
            ID = -1;
        }

        public UpdateInsuranceDTO(int patientID, string providerName, decimal coverage, int createdByReceptionistID)
        {
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