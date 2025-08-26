using AH.Application.DTOs.Row;

namespace AH.Application.DTOs.Entities
{
    public class TestOrderDTO
    {
        public int ID { get; set; }
        public AppointmentRowDTO Appointment { get; set; }
        public TestTypeRowDTO TestType { get; set; }

        public TestOrderDTO()
        {
            ID = -1;
            Appointment = new AppointmentRowDTO();
            TestType = new TestTypeRowDTO();
        }

        public TestOrderDTO(int id, AppointmentRowDTO appointment, TestTypeRowDTO testType)
        {
            ID = id;
            Appointment = appointment;
            TestType = testType;
        }
    }
}