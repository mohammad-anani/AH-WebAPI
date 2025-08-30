using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Filter
{
    public class InsuranceFilterDTO
    {
        [Required]
        public int PatientID { get; set; }

        public int? Page { get; set; }
    }
}