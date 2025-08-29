namespace AH.Application.DTOs.Filter.Helpers
{
    public interface IReceptionistAudit
    {
        public int? CreatedByReceptionistID { get; set; }

        public DateTime? CreatedAtFrom { get; set; }

        public DateTime? CreatedAtTo { get; set; }
    }
}