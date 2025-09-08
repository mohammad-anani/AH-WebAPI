using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    /// <summary>
    /// DTO used to create a payment that is tied directly to a service based entity (Appointment, TestAppointment, Operation)
    /// where the underlying Service/Bill is inferred from the entity ID (stored procedures resolve the ServiceID internally).
    /// </summary>
    public class CreateServicePaymentDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }

        /// <summary>
        /// Payment method name (e.g. Cash, Card, Insurance, etc.) that will be mapped to tinyint in DB.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Method { get; set; } = string.Empty;

        [BindNever]
        public int CreatedByReceptionistID { get; set; }
    }
}
