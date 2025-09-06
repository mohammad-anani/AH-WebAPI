using AH.Domain.Entities.Audit;

namespace AH.Domain.Entities
{
    public enum WorkingDaysEnum
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64
    }

    public class Employee
    {
        public static readonly Dictionary<string, string> ValidDays = new(StringComparer.OrdinalIgnoreCase)
    {
        {"mon", "Monday"}, {"monday", "Monday"},
        {"tue", "Tuesday"}, {"tuesday", "Tuesday"},
        {"wed", "Wednesday"}, {"wednesday", "Wednesday"},
        {"thu", "Thursday"}, {"thursday", "Thursday"},
        {"fri", "Friday"}, {"friday", "Friday"},
        {"sat", "Saturday"}, {"saturday", "Saturday"},
        {"sun", "Sunday"}, {"sunday", "Sunday"},
    };

        public Person Person { get; set; }

        public Department Department { get; set; }

        public int Salary { get; set; }

        public DateOnly HireDate { get; set; }

        public DateOnly? LeaveDate { get; set; }

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
            HireDate = DateOnly.MinValue;
            LeaveDate = null;
            WorkingDays = -1;
            ShiftStart = TimeOnly.MinValue;
            ShiftEnd = TimeOnly.MinValue;
            CreatedByAdmin = new AdminAudit(); // Fix: Don't create new AdminAudit to avoid circular dependency
            CreatedAt = DateTime.MinValue;
        }

        public Employee(Person person, Department department, int salary, DateOnly hireDate, DateOnly? leaveDate, int workingDays, TimeOnly shiftStart, TimeOnly shiftEnd, AdminAudit? createdByAdmin, DateTime createdAt)
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

        public Employee(Person person, Department department, int salary, DateOnly hireDate, int workingDays, TimeOnly shiftStart, TimeOnly shiftEnd, AdminAudit? createdByAdmin)
        {
            Person = person;
            Department = department;
            Salary = salary;
            HireDate = hireDate;
            LeaveDate = null;
            WorkingDays = workingDays;
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = DateTime.MinValue;
        }

        public static int ToBitmask(List<string> workingDays)
        {
            if (workingDays == null || workingDays.Count == 0)
                return -1;

            int bitmask = 0;

            foreach (var part in workingDays)
            {
                var trimmed = part.Trim();

                if (Enum.TryParse<WorkingDaysEnum>(trimmed, ignoreCase: true, out var day))
                {
                    bitmask |= (int)day;
                }
                else if (ValidDays.TryGetValue(trimmed, out var normalized))
                {
                    // Try again using normalized day name from ValidDays dictionary
                    if (Enum.TryParse<WorkingDaysEnum>(normalized, ignoreCase: true, out var normalizedDay))
                    {
                        bitmask |= (int)normalizedDay;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }

            return bitmask;
        }

        public static string FromBitmask(int bitmask)
        {
            if (bitmask <= 0)
                return string.Empty;

            var selectedDays = new List<string>();

            foreach (WorkingDaysEnum day in Enum.GetValues(typeof(WorkingDaysEnum)))
            {
                if ((bitmask & (int)day) != 0)
                {
                    selectedDays.Add(day.ToString());
                }
            }

            return string.Join(",", selectedDays);
        }
    }
}