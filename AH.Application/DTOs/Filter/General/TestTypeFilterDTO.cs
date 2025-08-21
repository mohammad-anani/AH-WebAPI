using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class TestTypeFilterDTO:IFilterable
    {
        public string? Name { get; set; }
        public int? DepartmentId { get; set; }
        public int? CostFrom { get; set; }
        public int? CostTo { get; set; }
        public int? CreatedByAdminId { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }
    }
}