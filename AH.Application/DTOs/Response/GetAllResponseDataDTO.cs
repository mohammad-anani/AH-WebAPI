namespace AH.Application.DTOs.Response
{
    public class GetAllResponseDataDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }

        public GetAllResponseDataDTO(IEnumerable<T> items, int count)
        {
            Items = items;
            Count = count;
        }

        public GetAllResponseDataDTO(GetAllResponseDTO<T> dto)
        {
            Items = dto.Items;
            Count = dto.Count;
        }
    }
}