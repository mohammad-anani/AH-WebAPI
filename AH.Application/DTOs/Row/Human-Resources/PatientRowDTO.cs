namespace AH.Application.DTOs.Row
{
    public class PatientRowDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public PatientRowDTO(int id, string fullName, int age, string phone)
        {
            ID = id;
            FullName = fullName;
            Age = age;
            Phone = phone;
        }

        public PatientRowDTO()
        {
            ID = -1;
            FullName = string.Empty;
            Age = -1;
            Phone = string.Empty;
        }
    }
}