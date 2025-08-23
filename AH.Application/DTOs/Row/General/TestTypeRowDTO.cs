namespace AH.Application.DTOs.Row
{
    public class TestTypeRowDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public int Cost { get; set; }

        public TestTypeRowDTO(int id, string name, string departmentName, int cost)
        {
            Id = id;
            Name = name;
            DepartmentName = departmentName;
            Cost = cost;
        }

        public TestTypeRowDTO()
        {
            Id = -1;
            Name = string.Empty;
            DepartmentName = string.Empty;
            Cost = 0;
        }
    }
}