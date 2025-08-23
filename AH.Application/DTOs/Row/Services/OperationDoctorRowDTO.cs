namespace AH.Application.DTOs.Row
{
    public class OperationDoctorRowDTO
    {
        public int ID { get; set; }
        public string OperationType { get; set; }
        public string DoctorFullName { get; set; }
        public string Role { get; set; }

        public OperationDoctorRowDTO(int id, string operationType, string doctorFullName, string role)
        {
            ID = id;
            OperationType = operationType;
            DoctorFullName = doctorFullName;
            Role = role;
        }

        public OperationDoctorRowDTO()
        {
            ID = -1;
            OperationType = string.Empty;
            DoctorFullName = string.Empty;
            Role = string.Empty;
        }
    }
}