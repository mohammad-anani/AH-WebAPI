using AH.Application.DTOs.Row;

namespace AH.Application.DTOs.Entities
{
    public class TestTypeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DepartmentRowDTO Department { get; set; }
        public int Cost { get; set; }
        public AdminRowDTO CreatedByAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public TestTypeDTO()
        {
            ID = -1;
            Name = string.Empty;
            Department = new DepartmentRowDTO();
            Cost = -1;
            CreatedByAdmin = new AdminRowDTO();
            CreatedAt = DateTime.MinValue;
        }

        public TestTypeDTO(int id, string name, DepartmentRowDTO department, int cost, AdminRowDTO createdByAdmin, DateTime createdAt)
        {
            ID = id;
            Name = name;
            Department = department;
            Cost = cost;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = createdAt;
        }
    }
}