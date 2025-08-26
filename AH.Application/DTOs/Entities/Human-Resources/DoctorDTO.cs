namespace AH.Application.DTOs.Entities
{
    public class DoctorDTO
    {
        public int ID { get; set; }
        public EmployeeDTO Employee { get; set; }
        public int CostPerAppointment { get; set; }
        public string Specialization { get; set; }

        public DoctorDTO()
        {
            ID = -1;
            Employee = new EmployeeDTO();
            CostPerAppointment = -1;
            Specialization = string.Empty;
        }

        public DoctorDTO(int id, EmployeeDTO employee, int costPerAppointment, string specialization)
        {
            ID = id;
            Employee = employee;
            CostPerAppointment = costPerAppointment;
            Specialization = specialization;
        }
    }
}