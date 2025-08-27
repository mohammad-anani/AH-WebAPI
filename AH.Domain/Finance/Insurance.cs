namespace AH.Domain.Entities
{
    public class Insurance
    {
        public int ID { get; set; }

        public Patient Patient { get; set; }

        public string ProviderName { get; set; }

        public decimal Coverage { get; set; }

        public DateOnly ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public Receptionist CreatedByReceptionist { get; set; }

        public Insurance()
        {
            ID = -1;
            Patient = new Patient(); // Fix: Don't create new Patient to avoid circular dependency
            ProviderName = "";
            Coverage = -1;
            ExpirationDate = DateOnly.MinValue;
            IsActive = false;
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = new Receptionist(); // Fix: Don't create new Receptionist to avoid circular dependency
        }

        public Insurance(int id, Patient patient, string providerName, decimal coverage, DateOnly expirationDate, bool isActive, DateTime createdAt, Receptionist createdByReceptionist)
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

        public Insurance(int id, decimal coverage, DateOnly expirationDate)
        {
            ID = -1;
            Patient = new Patient();
            ProviderName = String.Empty;
            Coverage = coverage;
            ExpirationDate = expirationDate;
            IsActive = false;
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = new Receptionist();
        }
    }
}