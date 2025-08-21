using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Domain.Entities.Audit;

namespace AH.Domain.Entities
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public AdminAudit CreatedByAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public Department()
        {
            ID = -1;
            Name = "";
            Phone = "";
            CreatedByAdmin = new AdminAudit(); // Fix: Don't create new AdminAudit to avoid circular dependency
            CreatedAt = DateTime.MinValue;
        }

        public Department(int id, string name, string phone, AdminAudit createdByAdmin, DateTime createdAt)
        {
            ID = id;
            Name = name;
            Phone = phone;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = createdAt;
        }

        public Department(string name, string phone, AdminAudit createdByAdmin)
        {
            ID = -1;
            Name = name;
            Phone = phone;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = DateTime.MinValue;
        }
    }
}
