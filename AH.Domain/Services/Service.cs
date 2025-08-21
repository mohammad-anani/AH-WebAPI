using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Service
    {
        public int? ID { get; set; }
        public Patient Patient { get; set; }

        public DateTime ScheduledDate { get; set; }

        public DateTime? ActualStartingDate { get; set; }

        public string Reason { get; set; }

        public string? Result { get; set; }

        public DateTime? ResultDate { get; set; }
        public string Status { get; set; }

        public string? Notes { get; set; }

        public Bill Bill { get; set; }

        public Receptionist CreatedByReceptionist { get; set; }

        public DateTime CreatedAt { get; set; }

        // Empty constructor
        public Service()
        {
            ID = null;
            Patient = new Patient();
            ScheduledDate = DateTime.MinValue;
            ActualStartingDate = null;
            Reason = "";
            Result = null;
            ResultDate = null;
            Status = "";
            Notes = null;
            Bill = new Bill();
            CreatedByReceptionist = new Receptionist();
            CreatedAt = DateTime.MinValue;
        }

        // All-fields constructor
        public Service(int id, Patient patient, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, Bill bill, Receptionist createdByReceptionist, DateTime createdAt)
        {
            ID = id;
            Patient = patient;
            ScheduledDate = scheduledDate;
            ActualStartingDate = actualStartingDate;
            Reason = reason;
            Result = result;
            ResultDate = resultDate;
            Status = status;
            Notes = notes;
            Bill = bill;
            CreatedByReceptionist = createdByReceptionist;
            CreatedAt = createdAt;
        }

        // Constructor without ID parameter
        public Service(Patient patient, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, Bill bill, Receptionist createdByReceptionist)
        {
            ID = null;
            Patient = patient;
            ScheduledDate = scheduledDate;
            ActualStartingDate = actualStartingDate;
            Reason = reason;
            Result = result;
            ResultDate = resultDate;
            Status = status;
            Notes = notes;
            Bill = bill;
            CreatedByReceptionist = createdByReceptionist;
            CreatedAt = DateTime.MinValue;
        }
    }
    }
