namespace AH.Application.DTOs.Entities
{
    public class AdminDTO
    {
        public int ID { get; set; }
        public EmployeeDTO Employee { get; set; }

        public AdminDTO()
        {
            ID = -1;
            Employee = new EmployeeDTO();
        }

        public AdminDTO(int id, EmployeeDTO employee)
        {
            ID = id;
            Employee = employee;
        }
    }
}