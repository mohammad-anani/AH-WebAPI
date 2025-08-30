using AH.Domain.Entities.Audit;

namespace AH.Domain.Entities
{
    public class DepartmentAudit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public AdminAudit CreatedByAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public DepartmentAudit()
        {
            ID = -1;
            Name = "";
            Phone = "";
            CreatedByAdmin = new AdminAudit(); // Fix: Don't create new AdminAudit to avoid circular dependency
            CreatedAt = DateTime.MinValue;
        }

        public DepartmentAudit(int id, string name, string phone, AdminAudit createdByAdmin, DateTime createdAt)
        {
            ID = id;
            Name = name;
            Phone = phone;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = createdAt;
        }

        public DepartmentAudit(int id)
        {
            ID = id;
            Name = "";
            Phone = "";
            CreatedByAdmin = new AdminAudit(); // Fix: Don't create new AdminAudit to avoid circular dependency
            CreatedAt = DateTime.MinValue;
        }

        public DepartmentAudit(string name, string phone, AdminAudit createdByAdmin)
        {
            ID = -1;
            Name = name;
            Phone = phone;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = DateTime.MinValue;
        }
    }
}