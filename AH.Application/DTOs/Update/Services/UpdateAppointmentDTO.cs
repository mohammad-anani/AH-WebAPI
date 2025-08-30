using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateAppointmentDTO : UpdateServiceDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Appointment ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Appointment ID must be a positive number")]
        public int ID { get; set; }

        public UpdateAppointmentDTO() : base()
        {
            ID = -1;
        }

        public UpdateAppointmentDTO(int? previousAppointmentID, int doctorID, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
            : base(patientID, scheduledDate, actualStartingDate, reason, result, resultDate, status, notes, billAmount, createdByReceptionistID)
        {
        }

        public Appointment ToAppointment()
        {
            PreviousAppointment? previousAppointment = null;

            return new Appointment(
                previousAppointment,
                new Doctor(-1),
                base.ToService()
            );
        }
    }
}