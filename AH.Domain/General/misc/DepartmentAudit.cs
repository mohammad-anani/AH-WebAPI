namespace AH.Domain.Entities.Audit
{
    public class DepartmentAudit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }

        public DepartmentAudit()
        {
            ID = -1;
            Name = "";
            Phone = "";
            CreatedAt = DateTime.MinValue;
        }

        public DepartmentAudit(int id, string name, string phone, DateTime createdAt)
        {
            ID = id;
            Name = name;
            Phone = phone;
            CreatedAt = createdAt;
        }

        public DepartmentAudit(int id)
        {
            ID = id;
            Name = "";
            Phone = "";
            CreatedAt = DateTime.MinValue;
        }

        public DepartmentAudit(string name, string phone, AdminAudit createdByAdmin)
        {
            ID = -1;
            Name = name;
            Phone = phone;
            CreatedAt = DateTime.MinValue;
        }
    }
}