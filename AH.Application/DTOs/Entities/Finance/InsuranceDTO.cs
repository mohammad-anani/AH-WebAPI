using AH.Application.DTOs.Row;

namespace AH.Application.DTOs.Entities
{
    public class InsuranceDTO
    {
        public int ID { get; set; }
        public PatientRowDTO Patient { get; set; }
        public string ProviderName { get; set; }
        public decimal Coverage { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReceptionistRowDTO CreatedByReceptionist { get; set; }

        public InsuranceDTO()
        {
            ID = -1;
            Patient = new PatientRowDTO();
            ProviderName = string.Empty;
            Coverage = -1;
            ExpirationDate = DateTime.MinValue;
            IsActive = false;
            CreatedAt = DateTime.MinValue;
            CreatedByReceptionist = new ReceptionistRowDTO();
        }

        public InsuranceDTO(int id, PatientRowDTO patient, string providerName, decimal coverage, DateTime expirationDate, bool isActive, DateTime createdAt, ReceptionistRowDTO createdByReceptionist)
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
    }
}