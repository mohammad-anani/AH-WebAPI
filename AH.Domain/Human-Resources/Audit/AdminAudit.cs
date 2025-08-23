namespace AH.Domain.Entities.Audit
{
    public class AdminAudit
    {
        public int ID { get; set; }
        public EmployeeAudit Employee { get; set; }

        public AdminAudit()
        {
            ID = -1;
            Employee = new EmployeeAudit();
        }

        public AdminAudit(int id, EmployeeAudit employee)
        {
            ID = id;
            Employee = employee;
        }

        public AdminAudit(EmployeeAudit employee)
        {
            ID = -1;
            Employee = employee;
        }
    }
}