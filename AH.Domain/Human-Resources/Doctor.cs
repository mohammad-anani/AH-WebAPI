using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Doctor
    {
        public int? ID { get; set; }

        public Employee Employee { get; set; }

        public int CostPerAppointment { get; set; }

        public string Specialization { get; set; }

        public Doctor()
        {
            ID = null;
            Employee = null; // Fix: Don't create new Employee to avoid circular dependency
            CostPerAppointment = -1;
            Specialization = "";
        }

        public Doctor(int id, Employee employee, int costPerAppointment, string specialization)
        {
            ID = id;
            Employee = employee;
            CostPerAppointment = costPerAppointment;
            Specialization = specialization;
        }

        public Doctor(Employee employee, int costPerAppointment, string specialization)
        {
            ID = null;
            Employee = employee;
            CostPerAppointment = costPerAppointment;
            Specialization = specialization;
        }
    }
}
