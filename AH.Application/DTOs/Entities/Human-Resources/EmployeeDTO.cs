using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.DTOs.Entities
{
    public class EmployeeDTO
    {
        public Person Person { get; set; }
        public DepartmentRowDTO? Department { get; set; }
        public int Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public List<string> WorkingDays { get; set; }
        public TimeOnly ShiftStart { get; set; }
        public TimeOnly ShiftEnd { get; set; }
        public AdminRowDTO? CreatedByAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public EmployeeDTO()
        {
            Person = new Person();
            Department = new DepartmentRowDTO();
            Salary = -1;
            HireDate = DateTime.MinValue;
            LeaveDate = null;
            WorkingDays = new List<string>();
            ShiftStart = TimeOnly.MinValue;
            ShiftEnd = TimeOnly.MinValue;
            CreatedByAdmin = new AdminRowDTO();
            CreatedAt = DateTime.MinValue;
        }

        public EmployeeDTO(Person person, DepartmentRowDTO department, int salary, DateTime hireDate, DateTime? leaveDate, List<string> workingDays, TimeOnly shiftStart, TimeOnly shiftEnd, AdminRowDTO? createdByAdmin, DateTime createdAt)
        {
            Person = person;
            Department = department.ID==-1?null:department;
            Salary = salary;
            HireDate = hireDate;
            LeaveDate = leaveDate;
            WorkingDays = workingDays;
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = createdAt;
        }
    }
}