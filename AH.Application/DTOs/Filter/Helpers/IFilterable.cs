namespace AH.Application.DTOs.Filter.Helpers
{
    public interface IFilterable
    {
        public string? Sort { get; set; }

        // Now a textual order: "asc", "desc", or null
        public string? Order { get; set; }

        public int? Page { get; set; }
    }
}