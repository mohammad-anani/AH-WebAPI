namespace AH.Application.DTOs.Row
{
    public class PatientRowDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        public PatientRowDTO(int id, string fullName)
        {
            ID = id;
            FullName = fullName;
        }

        public PatientRowDTO()
        {
            ID = -1;
            FullName = string.Empty;
        }
    }
}