using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class AdminFilterDTO : EmployeeFilter, IFilterable
    {

       public string? Sort {  get; set; }
        public bool? Order { get; set; }
        public int? Page {  get; set; }
    }
}