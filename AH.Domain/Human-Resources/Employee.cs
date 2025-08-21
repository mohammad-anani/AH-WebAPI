using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Domain.Entities.Audit;

namespace AH.Domain.Entities
{
    public class Employee
    {
        public int ID { get; set; }

        public Person Person { get; set; }

        public Department Department { get; set; }

        public int Salary { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime? LeaveDate { get; set; }

        public int WorkingDays { get; set; }

        public DateTime ShiftStart { get; set; }

        public DateTime ShiftEnd { get; set; }

        public AdminAudit CreatedByAdmin { get; set; }   


        public DateTime CreatedAt { get; set; }

        public Employee()
        {
            ID = -1;
            Person = new Person();
            Department = new Department(); // Fix: Don't create new Department to avoid circular dependency
            Salary = -1;
            HireDate = DateTime.MinValue;
            LeaveDate = null;
            WorkingDays = -1;
            ShiftStart = DateTime.MinValue;
            ShiftEnd = DateTime.MinValue;
            CreatedByAdmin = new AdminAudit(); // Fix: Don't create new AdminAudit to avoid circular dependency
            CreatedAt = DateTime.MinValue;
        }

        public Employee(int id, Person person, Department department, int salary, DateTime hireDate, DateTime? leaveDate, int workingDays, DateTime shiftStart, DateTime shiftEnd, AdminAudit createdByAdmin, DateTime createdAt)
        {
            ID = id;
            Person = person;
            Department = department;
            Salary = salary;
            HireDate = hireDate;
            LeaveDate = leaveDate;
            WorkingDays = workingDays;
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = createdAt;
        }

        public Employee(Person person, Department department, int salary, DateTime hireDate, DateTime? leaveDate, int workingDays, DateTime shiftStart, DateTime shiftEnd, AdminAudit createdByAdmin)
        {
            ID = -1;
            Person = person;
            Department = department;
            Salary = salary;
            HireDate = hireDate;
            LeaveDate = leaveDate;
            WorkingDays = workingDays;
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = DateTime.MinValue;
        }
    }
}
