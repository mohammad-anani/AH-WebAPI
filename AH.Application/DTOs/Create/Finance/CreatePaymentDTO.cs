using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreatePaymentDTO
    {
        public int BillID { get; set; }
        public int Amount { get; set; }
        public string Method { get; set; }
        public int CreatedByReceptionistID { get; set; }

        public CreatePaymentDTO()
        {
            BillID = -1;
            Amount = 0;
            Method = string.Empty;
            CreatedByReceptionistID = -1;
        }

        public CreatePaymentDTO(int billID, int amount, string method, int createdByReceptionistID)
        {
            BillID = billID;
            Amount = amount;
            Method = method;
            CreatedByReceptionistID = createdByReceptionistID;
        }

        public Payment ToPayment()
        {
            return new Payment(
                new Bill(BillID, 0, 0), // We only need the ID for the Bill reference
                Amount,
                Method,
                new Receptionist(CreatedByReceptionistID)
            );
        }
    }
}