using AH.Application.DTOs.Filter.Helpers;

namespace AH.Application.DTOs.Filter
{
    public class DepartmentFilterDTO : IFilterable, IAdminAudit
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public int? CreatedByAdminID { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public DepartmentFilterDTO(
            string? name,
            string? phone,
            int? createdByAdminID,
            DateTime? createdAtFrom,
            DateTime? createdAtTo,
            string? sort,
            bool? order,
            int? page)
        {
            Name = name;
            Phone = phone;
            CreatedByAdminID = createdByAdminID;
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
            CreatedByAdminID = null;
            CreatedAtFrom = null;
            CreatedAtTo = null;
            Sort = null;
            Order = null;
            Page = null;
        }
    }
}