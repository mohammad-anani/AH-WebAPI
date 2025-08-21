using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities.Audit
{
    public class AdminAudit
    {

        public int? ID { get; set; }
        public EmployeeAudit Employee { get; set; }

        public AdminAudit()
        {
            ID = null;
            Employee = new EmployeeAudit();
        }

        public AdminAudit(int id, EmployeeAudit employee)
        {
            ID = id;
            Employee = employee;
        }

        public AdminAudit(EmployeeAudit employee)
        {
            ID = null;
            Employee = employee;
        }
    }
}
