using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class OperationFilterDTO:ServiceFilter,IFilterable
    {
public string? Name { get; set; }

        public string? Description { get; set; }

        public int? DepartmentID { get; set; }
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

    }
}