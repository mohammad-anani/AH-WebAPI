using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Bill
    {
        public int? ID { get; set; }
        public int Amount { get; set; }

        public Bill()
        {
            ID = null;
            Amount = -1;
        }

        public Bill(int id, int amount)
        {
            ID = id;
            Amount = amount;
        }

        public Bill(int amount)
        {
            ID = null;
            Amount = amount;
        }
    }
}
