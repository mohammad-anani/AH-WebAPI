namespace AH.Application.DTOs.Response
{
    public class GetAllResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }

        public Exception? Exception { get; set; }

        public GetAllResponseDTO(IEnumerable<T> items, int count, Exception? exception)
        {
            Items = items;
            Count = count;
            Exception = exception;
        }
    }
}