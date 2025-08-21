using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class AppointmentFilterDTO:ServiceFilter, IFilterable
    {

        public int? PreviousAppointmentID { get; set; }

        public int? DoctorID { get; set; }


        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

    }
}