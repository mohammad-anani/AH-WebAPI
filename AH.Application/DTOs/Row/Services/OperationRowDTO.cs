using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Row
{
    public class OperationRowDTO
    {
        public int ID { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public string OperationType { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string Status { get; set; }

        public OperationRowDTO(int id, string patientFullName, string doctorFullName, string operationType, DateTime operationDateTime, string status)
        {
            ID = id;
            PatientFullName = patientFullName;
            DoctorFullName = doctorFullName;
            OperationType = operationType;
            OperationDateTime = operationDateTime;
            Status = status;
        }

        public OperationRowDTO()
        {
            ID = -1;
            PatientFullName = string.Empty;
            DoctorFullName = string.Empty;
            OperationType = string.Empty;
            OperationDateTime = DateTime.MinValue;
            Status = string.Empty;
        }
    }
}