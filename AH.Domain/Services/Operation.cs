namespace AH.Domain.Entities
{
    public class Operation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public string Description { get; set; }
        public Service Service { get; set; }

        public Operation()
        {
            ID = -1;
            Name = "";
            Department = new Department();
            Description = "";
            Service = new Service();
        }

        public Operation(int id, string name, Department department, string description, Service service)
        {
            ID = id;
            Name = name;
            Department = department;
            Description = description;
            Service = service;
        }

        public Operation(string name, Department department, string description, Service service)
        {
            ID = -1;
            Name = name;
            Department = department;
            Description = description;
            Service = service;
        }
    }
}