using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Row
{
    public class InsuranceRowDTO
    {
        public int ID { get; set; }
        public string ProviderName { get; set; }
        public decimal Coverage { get; set; }

        public bool IsActive { get; set; }

        public InsuranceRowDTO(int id, string providerName, decimal coverage, bool isActive)
        {
            ID = id;
            ProviderName = providerName;
            Coverage = coverage;
            IsActive = isActive;
        }

        public InsuranceRowDTO()
        {
            ID = -1;
            ProviderName = string.Empty;
            Coverage = 0;
            IsActive = false;
        }

    }
}