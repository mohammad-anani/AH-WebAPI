using AH.Domain.Entities.Audit;

namespace AH.Domain.Entities
{
    public class TestType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public int Cost { get; set; }
        public AdminAudit CreatedByAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public TestType()
        {
            ID = -1;
            Name = "";
            Department = new Department(); // Fix: Don't create new Department to avoid circular dependency
            Cost = -1;
            CreatedByAdmin = new AdminAudit(); // Fix: Don't create new Admin to avoid circular dependency
            CreatedAt = DateTime.MinValue;
        }

        public TestType(int id, string name, Department department, int cost, AdminAudit createdByAdmin, DateTime createdAt)
        {
            ID = id;
            Name = name;
            Department = department;
            Cost = cost;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = createdAt;
        }

        public TestType(int id)
        {
            ID = id;
            Name = "";
            Department = new Department();
            Cost = -1;
            CreatedByAdmin = new AdminAudit();
            CreatedAt = DateTime.MinValue;
        }

        public TestType(string name, Department department, int cost, AdminAudit createdByAdmin)
        {
            ID = -1;
            Name = name;
            Department = department;
            Cost = cost;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = DateTime.MinValue;
        }
    }
}