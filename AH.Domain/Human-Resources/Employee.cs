using AH.Domain.Entities.Audit;

namespace AH.Domain.Entities
{
    public class Employee
    {
        public Person Person { get; set; }

        public Department Department { get; set; }

        public int Salary { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime? LeaveDate { get; set; }

        public int WorkingDays { get; set; }

        public TimeOnly ShiftStart { get; set; }

        public TimeOnly ShiftEnd { get; set; }

        public AdminAudit? CreatedByAdmin { get; set; }

        public DateTime CreatedAt { get; set; }

        public Employee()
        {
            Person = new Person();
            Department = new Department(); // Fix: Don't create new Department to avoid circular dependency
            Salary = -1;
            HireDate = DateTime.MinValue;
            LeaveDate = null;
            WorkingDays = -1;
            ShiftStart = TimeOnly.MinValue;
            ShiftEnd = TimeOnly.MinValue;
            CreatedByAdmin = new AdminAudit(); // Fix: Don't create new AdminAudit to avoid circular dependency
            CreatedAt = DateTime.MinValue;
        }

        public Employee(Person person, Department department, int salary, DateTime hireDate, DateTime? leaveDate, int workingDays, TimeOnly shiftStart, TimeOnly shiftEnd, AdminAudit? createdByAdmin, DateTime createdAt)
        {
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

        public Employee(Person person, Department department, int salary, DateTime hireDate, DateTime? leaveDate, int workingDays, TimeOnly shiftStart, TimeOnly shiftEnd, AdminAudit? createdByAdmin)
        {
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