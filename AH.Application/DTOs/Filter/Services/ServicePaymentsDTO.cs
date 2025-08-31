using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AH.Application.DTOs.Filter
{
    public class ServicePaymentsDTO
    {
        [BindNever]
        public int ID { get; set; }

        public int? Page { get; set; }
    }
}