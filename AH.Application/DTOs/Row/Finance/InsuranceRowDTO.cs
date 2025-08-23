namespace AH.Application.DTOs.Row
{
    public class InsuranceRowDTO
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public string CoverageType { get; set; }

        public InsuranceRowDTO(int id, string companyName, string policyNumber, string coverageType)
        {
            ID = id;
            CompanyName = companyName;
            PolicyNumber = policyNumber;
            CoverageType = coverageType;
        }

        public InsuranceRowDTO()
        {
            ID = -1;
            CompanyName = string.Empty;
            PolicyNumber = string.Empty;
            CoverageType = string.Empty;
        }
    }
}