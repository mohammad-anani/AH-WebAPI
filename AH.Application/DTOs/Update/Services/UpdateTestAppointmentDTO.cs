using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateTestAppointmentDTO : UpdateServiceDTO
    {
        [Required(ErrorMessage = "Test order ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Test order ID must be a positive number")]
        public int TestOrderID { get; set; }

        [Required(ErrorMessage = "Test type ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Test type ID must be a positive number")]
        public int TestTypeID { get; set; }

        public UpdateTestAppointmentDTO() : base()
        {
            TestOrderID = -1;
            TestTypeID = -1;
        }

        public UpdateTestAppointmentDTO(int testOrderID, int testTypeID, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
            : base(patientID, scheduledDate, actualStartingDate, reason, result, resultDate, status, notes, billAmount, createdByReceptionistID)
        {
            TestOrderID = testOrderID;
            TestTypeID = testTypeID;
        }

        public TestAppointment ToTestAppointment()
        {
            return new TestAppointment(
                new TestOrder(TestOrderID),
                new TestType(TestTypeID),
                base.ToService()
            );
        }
    }
}