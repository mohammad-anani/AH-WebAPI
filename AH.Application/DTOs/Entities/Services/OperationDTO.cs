using AH.Application.DTOs.Row;

namespace AH.Application.DTOs.Entities
{
    public class OperationDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DepartmentRowDTO Department { get; set; }
        public string Description { get; set; }
        public ServiceDTO Service { get; set; }

        public OperationDTO()
        {
            ID = -1;
            Name = string.Empty;
            Department = new DepartmentRowDTO();
            Description = string.Empty;
            Service = new ServiceDTO();
        }

        public OperationDTO(int id, string name, DepartmentRowDTO department, string description, ServiceDTO service)
        {
            ID = id;
            Name = name;
            Department = department;
            Description = description;
            Service = service;
        }
    }
}