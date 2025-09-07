using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateTestAppointmentDTO : CreateServiceDTO
    {
        [Required(ErrorMessage = "Test type ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Test type ID must be a positive number")]
        public int TestTypeID { get; set; }

        public CreateTestAppointmentDTO() : base()
        {
            TestTypeID = -1;
        }

        public CreateTestAppointmentDTO(int testOrderID, int testTypeID, int patientID, DateTime scheduledDate, string reason, string? notes, int billAmount, int createdByReceptionistID)
            : base(patientID, scheduledDate, reason, notes, billAmount, createdByReceptionistID)
        {
            TestTypeID = testTypeID;
        }

        public TestAppointment ToTestAppointment()
        {
            return new TestAppointment(
                new TestOrder(),
                new TestType(TestTypeID),
                base.ToService("Scheduled")
            );
        }
    }
}