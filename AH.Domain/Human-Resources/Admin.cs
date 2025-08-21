using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Admin
    {
        public int? ID { get; set; }
        public Employee Employee { get; set; }

        public Admin()
        {
            ID = null;
            Employee = null; // Fix: Don't create new Employee to avoid circular dependency
        }

        public Admin(int id, Employee employee)
        {
            ID = id;
            Employee = employee;
        }

        public Admin(Employee employee)
        {
            ID = null;
            Employee = employee;
        }
    }
}
