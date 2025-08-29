using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateAppointmentDTO : UpdateServiceDTO
    {
        public int ID { get; set; }

        public UpdateAppointmentDTO() : base()
        {
            PreviousAppointmentID = null;
            DoctorID = -1;
        }

        public UpdateAppointmentDTO(int? previousAppointmentID, int doctorID, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
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