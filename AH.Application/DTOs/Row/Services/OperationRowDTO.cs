namespace AH.Application.DTOs.Row
{
    public class OperationRowDTO
    {
        public int ID { get; set; }
        public string PatientFullName { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string Status { get; set; }

        public OperationRowDTO(int id, string patientFullName, DateTime operationDateTime, string status)
        {
            ID = id;
            PatientFullName = patientFullName;
            OperationDateTime = operationDateTime;
            Status = status;
        }

        public OperationRowDTO()
        {
            ID = -1;
            PatientFullName = string.Empty;
            OperationDateTime = DateTime.MinValue;
            Status = string.Empty;
        }
    }
}