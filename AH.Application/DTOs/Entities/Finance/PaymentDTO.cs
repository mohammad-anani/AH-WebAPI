using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.DTOs.Entities
{
    public class PaymentDTO
    {
        public int ID { get; set; }
        public Bill Bill { get; set; }
        public int Amount { get; set; }
        public string Method { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReceptionistRowDTO CreatedByReceptionist { get; set; }

        public PaymentDTO()
        {
            ID = -1;
            Bill = new Bill();
            Amount = -1;
            Method = string.Empty;
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = new ReceptionistRowDTO();
        }

        public PaymentDTO(int id, Bill bill, int amount, string method, DateTime createdAt, ReceptionistRowDTO createdByReceptionist)
        {
            ID = id;
            Bill = bill;
            Amount = amount;
            Method = method;
            CreatedAt = createdAt;
            CreatedByReceptionist = createdByReceptionist;
        }
    }
}