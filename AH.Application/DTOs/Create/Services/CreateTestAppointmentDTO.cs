using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateTestAppointmentDTO : CreateServiceDTO
    {
        public int TestOrderID { get; set; }
        public int TestTypeID { get; set; }

        public CreateTestAppointmentDTO() : base()
        {
            TestOrderID = -1;
            TestTypeID = -1;
        }

        public CreateTestAppointmentDTO(int testOrderID, int testTypeID, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
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