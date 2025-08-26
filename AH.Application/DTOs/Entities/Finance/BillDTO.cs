namespace AH.Application.DTOs.Entities
{
    public class BillDTO
    {
        public int ID { get; set; }
        public int Amount { get; set; }

        public int AmountPaid { get; set; }

        public BillDTO()
        {
            ID = -1;
            Amount = -1;
            AmountPaid = -1;
        }

        public BillDTO(int id, int amount, int amountPaid)
        {
            ID = id;
            Amount = amount;
            AmountPaid = amountPaid;
        }
    }
}