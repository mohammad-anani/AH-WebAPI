namespace AH.Application.DTOs.Entities
{
    public class ReceptionistDTO
    {
        public int ID { get; set; }
        public EmployeeDTO Employee { get; set; }

        public ReceptionistDTO()
        {
            ID = -1;
            Employee = new EmployeeDTO();
        }

        public ReceptionistDTO(int id, EmployeeDTO employee)
        {
            ID = id;
            Employee = employee;
        }
    }
}