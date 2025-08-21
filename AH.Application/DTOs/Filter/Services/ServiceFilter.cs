using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class ServiceFilter
    {
        public int? PatientId { get; set; }
        public DateTime? ScheduledDateFrom { get; set; }
        public DateTime? ScheduledDateTo { get; set; }
        public DateTime? ActualStartingDateFrom { get; set; }
        public DateTime? ActualStartingDateTo { get; set; }
        public string? Reason { get; set; }
        public string? Result { get; set; }
        public DateTime? ResultDateFrom { get; set; }
        public DateTime? ResultDateTo { get; set; }
        public byte? Status { get; set; }
        public string? Notes { get; set; }
        public int? BillId { get; set; }
        public int? CreatedByReceptionistId { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
    }
}