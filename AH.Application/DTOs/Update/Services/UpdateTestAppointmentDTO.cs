using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateTestAppointmentDTO : UpdateServiceDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Test appointment ID is required")]
        public int ID { get; set; }

        public UpdateTestAppointmentDTO() : base()
        {
            ID = -1;
        }

        public UpdateTestAppointmentDTO(int testOrderID, int testTypeID, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
            : base(patientID, scheduledDate, actualStartingDate, reason, result, resultDate, status, notes, billAmount, createdByReceptionistID)
        {
        }

        public TestAppointment ToTestAppointment()
        {
            return new TestAppointment(ID,
                new TestOrder(-1),
                new TestType(-1),
                base.ToService()
            );
        }
    }
}