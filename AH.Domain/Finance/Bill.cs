namespace AH.Domain.Entities
{
    public class Bill
    {
        public int ID { get; set; }
        public int Amount { get; set; }

        public int? AmountPaid { get; set; }

        public Bill()
        {
            ID = -1;
            Amount = -1;
            AmountPaid = -1;
        }

        public Bill(int id, int amount, int? amountPaid)
        {
            ID = id;
            Amount = amount;
            AmountPaid = amountPaid;
        }
    }
}