namespace AH.Domain.Entities.Audit
{
    public class EmployeeAudit
    {
        public int ID { get; set; }

        public Person Person { get; set; }

        public DepartmentAudit Department { get; set; }

        public int Salary { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime? LeaveDate { get; set; }

        public int WorkingDays { get; set; }

        public DateTime ShiftStart { get; set; }

        public DateTime ShiftEnd { get; set; }

        public DateTime CreatedAt { get; set; }

        public EmployeeAudit()
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
            CreatedAt = DateTime.MinValue;
        }

        public EmployeeAudit(int id, Person person, DepartmentAudit department, int salary, DateTime hireDate, DateTime? leaveDate, int workingDays, DateTime shiftStart, DateTime shiftEnd, DateTime createdAt)
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
            CreatedAt = createdAt;
        }

        public EmployeeAudit(Person person, DepartmentAudit department, int salary, DateTime hireDate, DateTime? leaveDate, int workingDays, DateTime shiftStart, DateTime shiftEnd)
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
            CreatedAt = DateTime.MinValue;
        }
    }
}