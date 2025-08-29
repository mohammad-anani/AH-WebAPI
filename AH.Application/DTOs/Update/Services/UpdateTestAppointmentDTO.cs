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
        public int ID { get; set; }

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