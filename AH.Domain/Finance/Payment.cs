using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Payment
    {
        public int? ID { get; set; }
        public Bill Bill { get; set; }
        public int Amount { get; set; }
        public string Method { get; set; }
        public DateTime CreatedAt { get; set; }
        public Receptionist CreatedByReceptionist { get; set; }

        public Payment()
        {
            ID = null;
            Bill = new Bill();
            Amount = -1;
            Method = "";
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = null; // Fix: Don't create new Receptionist to avoid circular dependency
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
            ID = null;
            Bill = bill;
            Amount = amount;
            Method = method;
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = createdByReceptionist;
        }
    }
}
