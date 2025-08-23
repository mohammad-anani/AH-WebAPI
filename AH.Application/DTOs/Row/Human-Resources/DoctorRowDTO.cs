namespace AH.Application.DTOs.Row
{
    public class DoctorRowDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        public string Specialization { get; set; }

        public DoctorRowDTO(int id, string fullName, string specialization)
        {
            ID = id;
            FullName = fullName;
            Specialization = specialization;
        }

        public DoctorRowDTO()
        {
            ID = -1;

            FullName = string.Empty;
            Specialization = string.Empty;
        }
    }
}