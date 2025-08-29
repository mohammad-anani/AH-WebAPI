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
        [Required(ErrorMessage = "Doctor ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be a positive number")]
        public int DoctorID { get; set; }

        public CreateAppointmentDTO() : base()
        {
            DoctorID = -1;
        }

        public CreateAppointmentDTO(int? previousAppointmentID, int doctorID, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
            : base(patientID, scheduledDate, actualStartingDate, reason, result, resultDate, status, notes, billAmount, createdByReceptionistID)
        {
            DoctorID = doctorID;
        }

        public Appointment ToAppointment()
        {
            return new Appointment(

                new Doctor(DoctorID),
                base.ToService()
            );
        }
    }
}