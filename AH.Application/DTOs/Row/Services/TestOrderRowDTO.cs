namespace AH.Application.DTOs.Row
{
    public class TestOrderRowDTO
    {
        public int ID { get; set; }

        public string PatientFullName { get; set; }

        public string TestTypeName { get; set; }

        public TestOrderRowDTO(int id, string patientFullName, string testName)
        {
            ID = id;
            PatientFullName = patientFullName;
            TestTypeName = testName;
        }

        public TestOrderRowDTO()
        {
            ID = -1;
            PatientFullName = string.Empty;
            TestTypeName = string.Empty;
        }
    }
}