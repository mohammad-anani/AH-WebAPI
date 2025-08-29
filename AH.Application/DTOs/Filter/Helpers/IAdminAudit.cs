namespace AH.Application.DTOs.Filter.Helpers
{
    public interface IAdminAudit
    {
        public int? CreatedByAdminID { get; set; }

        public DateTime? CreatedAtFrom { get; set; }

        public DateTime? CreatedAtTo { get; set; }
    }
}