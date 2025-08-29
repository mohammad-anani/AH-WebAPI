using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateAppointmentDTO : CreateServiceDTO
    {
        public int? PreviousAppointmentID { get; set; }

        [Required(ErrorMessage = "Doctor ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be a positive number")]
        public int DoctorID { get; set; }

        public CreateAppointmentDTO() : base()
        {
            PreviousAppointmentID = null;
            DoctorID = -1;
        }

        public CreateAppointmentDTO(int? previousAppointmentID, int doctorID, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
            : base(patientID, scheduledDate, actualStartingDate, reason, result, resultDate, status, notes, billAmount, createdByReceptionistID)
        {
            PreviousAppointmentID = previousAppointmentID;
            DoctorID = doctorID;
        }

        public Appointment ToAppointment()
        {
            PreviousAppointment? previousAppointment = PreviousAppointmentID.HasValue 
                ? new PreviousAppointment(PreviousAppointmentID.Value) 
                : null;

            return new Appointment(
                previousAppointment,
                new Doctor(DoctorID),
                base.ToService()
            );
        }
    }
}