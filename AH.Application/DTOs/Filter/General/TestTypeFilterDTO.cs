using AH.Application.DTOs.Filter.Helpers;

namespace AH.Application.DTOs.Filter
{
    public class TestTypeFilterDTO : IFilterable, IAdminAudit
    {
        public string? Name { get; set; }
        public int? DepartmentID { get; set; }
        public int? CostFrom { get; set; }
        public int? CostTo { get; set; }
        public int? CreatedByAdminID { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public TestTypeFilterDTO(
            string? name,
            int? departmentID,
            int? costFrom,
            int? costTo,
            int? createdByAdminID,
            DateTime? createdAtFrom,
            DateTime? createdAtTo,
            string? sort,
            bool? order,
            int? page)
        {
            Name = name;
            DepartmentID = departmentID;
            CostFrom = costFrom;
            CostTo = costTo;
            CreatedByAdminID = createdByAdminID;
            CreatedAtFrom = createdAtFrom;
            CreatedAtTo = createdAtTo;
            Sort = sort;
            Order = order;
            Page = page;
        }

        // Parameterless constructor
        public TestTypeFilterDTO()
        {
            Name = null;
            DepartmentID = null;
            CostFrom = null;
            CostTo = null;
            CreatedByAdminID = null;
            CreatedAtFrom = null;
            CreatedAtTo = null;
            Sort = null;
            Order = null;
            Page = null;
        }
    }
}