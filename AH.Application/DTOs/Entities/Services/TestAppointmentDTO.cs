using AH.Application.DTOs.Row;

namespace AH.Application.DTOs.Entities
{
    public class TestAppointmentDTO
    {
        public int ID { get; set; }
        public TestOrderRowDTO? TestOrder { get; set; }
        public TestTypeRowDTO TestType { get; set; }
        public ServiceDTO Service { get; set; }

        public TestAppointmentDTO()
        {
            ID = -1;
            TestOrder = new TestOrderRowDTO();
            TestType = new TestTypeRowDTO();
            Service = new ServiceDTO();
        }

        public TestAppointmentDTO(int id, TestOrderRowDTO? testOrder, TestTypeRowDTO testType, ServiceDTO service)
        {
            ID = id;
            TestOrder = testOrder;
            TestType = testType;
            Service = service;
        }
    }
}