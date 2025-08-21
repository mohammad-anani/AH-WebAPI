using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class DoctorFilterDTO:EmployeeFilter,IFilterable
    {
        public string? Specialization {  get; set; }
        
        public int ? CostPerAppointmentFrom { get; set; }

        public int ? CostPerAppointmentTo { get; set; }

        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }
    }
}