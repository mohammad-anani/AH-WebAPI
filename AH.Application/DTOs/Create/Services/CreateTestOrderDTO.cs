using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateTestOrderDTO
    {
        [Required(ErrorMessage = "Appointment ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Appointment ID must be a positive number")]
        public int AppointmentID { get; set; }

        [Required(ErrorMessage = "Test type ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Test type ID must be a positive number")]
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