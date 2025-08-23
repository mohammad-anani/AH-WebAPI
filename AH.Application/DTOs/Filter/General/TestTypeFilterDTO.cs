namespace AH.Application.DTOs.Filter
{
    public class TestTypeFilterDTO : IFilterable
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

        // Full constructor
        public TestTypeFilterDTO(
            string? name,
            int? departmentId,
            int? costFrom,
            int? costTo,
            int? createdByAdminId,
            DateTime? createdAtFrom,
            DateTime? createdAtTo,
            string? sort,
            bool? order,
            int? page)
        {
            Name = name;
            DepartmentId = departmentId;
            CostFrom = costFrom;
            CostTo = costTo;
            CreatedByAdminId = createdByAdminId;
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
            DepartmentId = null;
            CostFrom = null;
            CostTo = null;
            CreatedByAdminId = null;
            CreatedAtFrom = null;
            CreatedAtTo = null;
            Sort = null;
            Order = null;
            Page = null;
        }
    }
}