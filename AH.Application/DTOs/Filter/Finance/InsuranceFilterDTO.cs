using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class InsuranceFilterDTO
    {
        public int? PatientID { get; set; }

        public int? Page { get; set; }
    }
}