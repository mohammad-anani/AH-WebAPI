using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Operation
    {
        public int? ID { get; set; }
        public int Name { get; set; }
        public Department Department { get; set; }   
        public string Description { get; set; } 
        public Service Service { get; set; }

        public Operation()
        {
            ID = null;
            Name = -1;
            Department = new Department();
            Description = "";
            Service = new Service();
        }

        public Operation(int id, int name, Department department, string description, Service service)
        {
            ID = id;
            Name = name;
            Department = department;
            Description = description;
            Service = service;
        }

        public Operation(int name, Department department, string description, Service service)
        {
            ID = null;
            Name = name;
            Department = department;
            Description = description;
            Service = service;
        }
    }
}
