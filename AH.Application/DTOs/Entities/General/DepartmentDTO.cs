using AH.Application.DTOs.Row;

namespace AH.Application.DTOs.Entities
{
    public class DepartmentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public AdminRowDTO CreatedByAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public DepartmentDTO()
        {
            ID = -1;
            Name = string.Empty;
            Phone = string.Empty;
            CreatedByAdmin = new AdminRowDTO();
            CreatedAt = DateTime.MinValue;
        }

        public DepartmentDTO(int id, string name, string phone, AdminRowDTO createdByAdmin, DateTime createdAt)
        {
            ID = id;
            Name = name;
            Phone = phone;
            CreatedByAdmin = createdByAdmin;
            CreatedAt = createdAt;
        }
    }
}