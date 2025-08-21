using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class DepartmentFilterDTO : IFilterable
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public int? CreatedByAdminId { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public DepartmentFilterDTO(
            string? name,
            string? phone,
            int? createdByAdminId,
            DateTime? createdAtFrom,
            DateTime? createdAtTo,
            string? sort,
            bool? order,
            int? page)
        {
            Name = name;
            Phone = phone;
            CreatedByAdminId = createdByAdminId;
            CreatedAtFrom = createdAtFrom;
            CreatedAtTo = createdAtTo;
            Sort = sort;
            Order = order;
            Page = page;
        }

        // Parameterless constructor
        public DepartmentFilterDTO()
        {
            Name = null;
            Phone = null;
            CreatedByAdminId = null;
            CreatedAtFrom = null;
            CreatedAtTo = null;
            Sort = null;
            Order = null;
            Page = null;
        }
    }
}