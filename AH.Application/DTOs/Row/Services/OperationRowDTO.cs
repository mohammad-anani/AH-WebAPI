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
        public string Name { get; set; }
        public string PatientFullName { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; }
        public bool IsPaid { get; set; }

        // Empty constructor with defaults
        public OperationRowDTO()
        {
            ID = 0;
            Name = string.Empty;
            PatientFullName = string.Empty;
            ScheduledDate = DateTime.MinValue;
            Status = string.Empty;
            IsPaid = false;
        }

        // Full constructor
        public OperationRowDTO(int id, string name, string patientFullName, DateTime scheduledDate, string status, bool isPaid)
        {
            ID = id;
            Name = name;
            PatientFullName = patientFullName;
            ScheduledDate = scheduledDate;
            Status = status;
            IsPaid = isPaid;
        }
    }

}