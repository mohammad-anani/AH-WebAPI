namespace AH.Application.DTOs.Filter
{
    public interface IFilterable
    {
        public string? Sort { get; set; }

        public bool? Order { get; set; }

        public int? Page { get; set; }
    }
}