namespace AH.Application.DTOs.Row
{
    public class OperationDoctorRowDTO
    {
        public int ID { get; set; }
        public int DoctorID { get; set; }
        public string DoctorFullName { get; set; }
        public string Role { get; set; }

        public OperationDoctorRowDTO(int id, int doctorID, string doctorFullName, string role)
        {
            ID = id;
            DoctorID = doctorID;
            DoctorFullName = doctorFullName;
            Role = role;
        }

        public OperationDoctorRowDTO()
        {
            ID = -1;
            DoctorID = -1;
            DoctorFullName = string.Empty;
            Role = string.Empty;
        }
    }
}