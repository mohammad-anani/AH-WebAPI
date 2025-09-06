using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreatePaymentDTO : PaymentFormDTO
    {
        [Required(ErrorMessage = "Bill ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Bill ID must be a positive number")]
        public int BillID { get; set; }

        [BindNever]
        [Required(ErrorMessage = "Created by receptionist ID is required")]
        public int CreatedByReceptionistID { get; set; }

        public CreatePaymentDTO()
        {
            BillID = -1;
            Amount = 0;
            Method = "";
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
                new Bill(BillID, 0, 0),
                Amount,
                Method,
                new Receptionist(CreatedByReceptionistID)
            );
        }
    }
}