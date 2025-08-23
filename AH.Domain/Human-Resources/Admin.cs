namespace AH.Domain.Entities
{
    public class Admin
    {
        public int ID { get; set; }
        public Employee Employee { get; set; }

        public Admin()
        {
            ID = -1;
            Employee = new Employee(); // Fix: Don't create new Employee to avoid circular dependency
        }

        public Admin(int id, Employee employee)
        {
            ID = id;
            Employee = employee;
        }

        public Admin(Employee employee)
        {
            ID = -1;
            Employee = employee;
        }
    }
}