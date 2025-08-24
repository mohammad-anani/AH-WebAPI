using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter.Finance
{
    public class PaymentFilterDTO
    {
        public int? BillID { get; set; }

        public int? Page { get; set; }
    }
}