using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class TestAppointment
    {
        public int ID { get; set; } 

        public TestOrder TestOrder { get; set; }

        public TestType TestType { get; set; }

        public Service Service { get; set; }

        public TestAppointment()
        {
            ID = -1;
            TestOrder = new TestOrder();
            TestType = new TestType();
            Service = new Service();
        }

        public TestAppointment(int id, TestOrder testOrder, TestType testType, Service service)
        {
            ID = id;
            TestOrder = testOrder;
            TestType = testType;
            Service = service;
        }

        public TestAppointment(TestOrder testOrder, TestType testType, Service service)
        {
            ID = -1;
            TestOrder = testOrder;
            TestType = testType;
            Service = service;
        }
    }
}
