namespace AH.Application.DTOs.Row
{
    public class PaymentRowDTO
    {
        public int ID { get; set; }

        public int Amount { get; set; }
        public string Method { get; set; }

        public PaymentRowDTO(int id, int amount, string method)
        {
            ID = id;
            Amount = amount;
            Method = method;
        }

        public PaymentRowDTO()
        {
            ID = -1;
            Amount = 0;
            Method = string.Empty;
        }
    }
}