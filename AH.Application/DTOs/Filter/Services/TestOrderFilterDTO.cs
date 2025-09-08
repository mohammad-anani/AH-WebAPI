using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AH.Application.DTOs.Filter
{
    public class TestOrderFilterDTO
    {
        [BindNever]
        public int? AppointmentID { get; set; }

        public int? Page { get; set; }
    }
}