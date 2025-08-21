using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class TestAppointmentFilterDTO:ServiceFilter,IFilterable
    {

        public int? TestOrderID { get; set; }

        public int? TestTypeID { get; set; }
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

        // Full constructor
        public TestAppointmentFilterDTO(
            int? testOrderID,
            int? testTypeID,
            string? sort,
            bool? order,
            int? page,
            int? patientId = null,
            DateTime? scheduledDateFrom = null,
            DateTime? scheduledDateTo = null,
            DateTime? actualStartingDateFrom = null,
            DateTime? actualStartingDateTo = null,
            string? reason = null,
            string? result = null,
            DateTime? resultDateFrom = null,
            DateTime? resultDateTo = null,
            byte? status = null,
            string? notes = null,
            int? billId = null,
            int? createdByReceptionistId = null,
            DateTime? createdAtFrom = null,
            DateTime? createdAtTo = null)
            : base(patientId, scheduledDateFrom, scheduledDateTo, actualStartingDateFrom, actualStartingDateTo,
                   reason, result, resultDateFrom, resultDateTo, status, notes, billId, createdByReceptionistId, createdAtFrom, createdAtTo)
        {
            TestOrderID = testOrderID;
            TestTypeID = testTypeID;
            Sort = sort;
            Order = order;
            Page = page;
        }

        // Parameterless constructor
        public TestAppointmentFilterDTO() : base()
        {
            TestOrderID = null;
            TestTypeID = null;
            Sort = null;
            Order = null;
            Page = null;
        }
    }
}