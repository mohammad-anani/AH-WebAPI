using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Insurance
    {
        public int ID { get; set; }

        public Patient Patient { get; set; }

        public string ProviderName { get; set; }

        public decimal Coverage { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public Receptionist CreatedByReceptionist { get; set; }

        public Insurance()
        {
            ID = -1;
            Patient = new Patient(); // Fix: Don't create new Patient to avoid circular dependency
            ProviderName = "";
            Coverage = -1;
            ExpirationDate = DateTime.MinValue;
            IsActive = false;
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = new Receptionist(); // Fix: Don't create new Receptionist to avoid circular dependency
        }

        public Insurance(int id, Patient patient, string providerName, decimal coverage, DateTime expirationDate, bool isActive, DateTime createdAt, Receptionist createdByReceptionist)
        {
            ID = id;
            Patient = patient;
            ProviderName = providerName;
            Coverage = coverage;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedAt = createdAt;
            CreatedByReceptionist = createdByReceptionist;
        }

        public Insurance(Patient patient, string providerName, decimal coverage, DateTime expirationDate, bool isActive, Receptionist createdByReceptionist)
        {
            ID = -1;
            Patient = patient;
            ProviderName = providerName;
            Coverage = coverage;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = createdByReceptionist;
        }
    }
}
