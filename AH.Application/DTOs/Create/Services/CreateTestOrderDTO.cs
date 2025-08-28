using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateTestOrderDTO
    {
        public int AppointmentID { get; set; }
        public int TestTypeID { get; set; }

        public CreateTestOrderDTO()
        {
            AppointmentID = -1;
            TestTypeID = -1;
        }

        public CreateTestOrderDTO(int appointmentID, int testTypeID)
        {
            AppointmentID = appointmentID;
            TestTypeID = testTypeID;
        }

        public TestOrder ToTestOrder()
        {
            return new TestOrder(
                new Appointment(AppointmentID),
                new TestType(TestTypeID)
            );
        }
    }
}