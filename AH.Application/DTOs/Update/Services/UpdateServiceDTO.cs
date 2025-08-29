using AH.Application.DTOs.Validation;
using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateServiceDTO
    {
        [Required(ErrorMessage = "Reason is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Reason must be at least 10 characters")]
        public string Reason { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Notes can be empty or any length")]
        public string? Notes { get; set; }

        public UpdateServiceDTO()
        {
            Reason = string.Empty;
            Notes = null;
        }

        public UpdateServiceDTO(int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
        {
            Reason = reason;
            Notes = notes;
        }

        public Service ToService()
        {
            return new Service(
                new Patient(-1),
                DateTime.MinValue,
                null,
                Reason,
                null,
                null,
                "",
                Notes,
                new Bill(-1, 0, 0),
                new Receptionist(-1)
            );
        }
    }
}