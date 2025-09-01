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

        public Employee(Person person, Department department, int salary, DateTime hireDate, int workingDays, TimeOnly shiftStart, TimeOnly shiftEnd, AdminAudit? createdByAdmin)
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

        public static int ToBitmask(string workingDaysString)
        {
            if (string.IsNullOrWhiteSpace(workingDaysString))
                return -1;

            int bitmask = 0;

            foreach (var part in workingDaysString.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                if (Enum.TryParse<WorkingDaysEnum>(part.Trim(), ignoreCase: true, out var day))
                {
                    bitmask |= (int)day;
                }
                else if (ValidDays.TryGetValue(part.Trim(), out var normalized))
                {
                    // Try again using normalized day name from ValidDays dictionary
                    if (Enum.TryParse<WorkingDaysEnum>(normalized, out var normalizedDay))
                    {
                        bitmask |= (int)normalizedDay;
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid working day: '{part}'.");
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