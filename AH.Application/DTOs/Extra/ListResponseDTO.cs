namespace AH.Application.DTOs.Extra
{
    public class ListResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }

        public Exception? Exception { get; set; }

        public ListResponseDTO(IEnumerable<T> items, int count, Exception? exception)
        {
            Items = items;
            Count = count;
            Exception = exception;
        }
    }
}