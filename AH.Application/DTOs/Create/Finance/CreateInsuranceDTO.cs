using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateInsuranceDTO
    {
        public int PatientID { get; set; }
        public string ProviderName { get; set; }
        public decimal Coverage { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByReceptionistID { get; set; }

        public CreateInsuranceDTO()
        {
            PatientID = -1;
            ProviderName = string.Empty;
            Coverage = 0;
            ExpirationDate = DateOnly.MinValue;
            IsActive = true;
            CreatedByReceptionistID = -1;
        }

        public CreateInsuranceDTO(int patientID, string providerName, decimal coverage, DateOnly expirationDate, bool isActive, int createdByReceptionistID)
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