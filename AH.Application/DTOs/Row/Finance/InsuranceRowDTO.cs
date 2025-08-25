namespace AH.Application.DTOs.Row
{
    public class InsuranceRowDTO
    {
        public int ID { get; set; }
        public string ProviderName { get; set; }
        public decimal Coverage { get; set; }

        public bool IsActive { get; set; }

        public InsuranceRowDTO(int iD, string providerName, decimal coverage, bool isActive)
        {
            ID = iD;
            ProviderName = providerName;
            Coverage = coverage;
            IsActive = isActive;
        }

        public InsuranceRowDTO()
        {
            ID = -1;
            ProviderName = String.Empty;
            Coverage = -1;
            IsActive = false;
        }
    }
}