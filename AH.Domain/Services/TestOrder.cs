using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class TestOrder
    {
        public int? ID { get; set; }
        public Appointment Appointment { get; set; }
        public TestType TestType { get; set; }

        public TestOrder()
        {
            ID = null;
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
            ID = null;
            Appointment = appointment;
            TestType = testType;
        }
    }
}
