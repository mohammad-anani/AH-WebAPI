namespace AH.Application.DTOs.Row
{
    public class TestTypeRowDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public int Cost { get; set; }

        public TestTypeRowDTO(int id, string name, string departmentName, int cost)
        {
            ID = id;
            Name = name;
            DepartmentName = departmentName;
            Cost = cost;
        }

        public TestTypeRowDTO()
        {
            ID = -1;
            Name = string.Empty;
            DepartmentName = string.Empty;
            Cost = 0;
        }
    }
}