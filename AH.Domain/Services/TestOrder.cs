namespace AH.Domain.Entities
{
    public class TestOrder
    {
        public int ID { get; set; }
        public Appointment Appointment { get; set; }
        public TestType TestType { get; set; }

        public TestOrder()
        {
            ID = -1;
            Appointment = new Appointment();
            TestType = new TestType();
        }

        public TestOrder(int id, Appointment appointment, TestType testType)
        {
            ID = id;
            Appointment = appointment;
            TestType = testType;
        }

        public TestOrder(Appointment appointment, TestType testType)
        {
            ID = -1;
            Appointment = appointment;
            TestType = testType;
        }
    }
}