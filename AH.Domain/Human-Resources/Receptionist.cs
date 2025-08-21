using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Receptionist
    {
        public int ID { get; set; }
        public Employee Employee { get; set; }

        public Receptionist()
        {
            ID = -1;
            Employee = new Employee(); // Fix: Don't create new Employee to avoid circular dependency
        }

        public Receptionist(int id, Employee employee)
        {
            ID = id;
            Employee = employee;
        }

        public Receptionist(Employee employee)
        {
            ID = -1;
            Employee = employee;
        }
    }
}
