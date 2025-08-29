using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreatePaymentDTO
    {
        [Required(ErrorMessage = "Bill ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Bill ID must be a positive number")]
        public int BillID { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(10, 9999, ErrorMessage = "Amount must be between 10 and 9,999")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        [Range(1, 3, ErrorMessage = "Method must be 1 (Card), 2 (Cash), or 3 (Insurance)")]
        public int Method { get; set; }

        [Required(ErrorMessage = "Created by receptionist ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Created by receptionist ID must be a positive number")]
        public int CreatedByReceptionistID { get; set; }

        public CreatePaymentDTO()
        {
            BillID = -1;
            Amount = 0;
            Method = -1;
            CreatedByReceptionistID = -1;
        }

        public CreatePaymentDTO(int billID, int amount, int method, int createdByReceptionistID)
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
                Payment.GetMethod(Method),
                new Receptionist(CreatedByReceptionistID)
            );
        }
    }
}