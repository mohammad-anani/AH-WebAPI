using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class TestAppointmentFilterDTO:ServiceFilter,IFilterable
    {

        public int? TestOrderID { get; set; }

        public int? TestTypeID { get; set; }
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

    }
}