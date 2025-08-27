namespace AH.Domain.Entities
{
    public class Payment
    {
        public int ID { get; set; }
        public Bill Bill { get; set; }
        public int Amount { get; set; }
        public string Method { get; set; }
        public DateTime CreatedAt { get; set; }
        public Receptionist CreatedByReceptionist { get; set; }

        public Payment()
        {
            ID = -1;
            Bill = new Bill();
            Amount = -1;
            Method = "";
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = new Receptionist(); // Fix: Don't create new Receptionist to avoid circular dependency
        }

        public Payment(int id, Bill bill, int amount, string method, DateTime createdAt, Receptionist createdByReceptionist)
        {
            ID = id;
            Bill = bill;
            Amount = amount;
            Method = method;
            CreatedAt = createdAt;
            CreatedByReceptionist = createdByReceptionist;
        }

        public Payment(Bill bill, int amount, string method, Receptionist createdByReceptionist)
        {
            ID = -1;
            Bill = bill;
            Amount = amount;
            Method = method;
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = createdByReceptionist;
        }

        public static int GetMethod(string Method)
        {
            switch (Method.ToLower())
            {
                case "Card": return 1;
                case "Cash": return 2;
                case "Insurance": return 3;
                default: return -1;
            }
        }

        public static string GetMethod(int method)
        {
            switch (method)
            {
                case 1: return "Card";
                case 2: return "Cash";
                case 3: return "Insurance";
                default: return "Invalid Method";
            }
        }
    }
}