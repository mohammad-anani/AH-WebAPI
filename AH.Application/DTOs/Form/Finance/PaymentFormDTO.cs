using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Form
{
    public class PaymentFormDTO
    {
        [Required(ErrorMessage = "Amount is required")]
        [Range(10, 9999, ErrorMessage = "Amount must be between 10 and 9,999")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        [Range(1, 3, ErrorMessage = "Method must be 1 (Card), 2 (Cash), or 3 (Insurance)")]
        public int Method { get; set; }
    }
}
