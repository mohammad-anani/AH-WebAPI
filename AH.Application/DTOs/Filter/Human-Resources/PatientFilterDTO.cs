using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Filter
{
    public class PatientFilterDTO:PersonFilter,IFilterable
    {
        public string? Sort { get; set; }
        public bool? Order { get; set; }
        public int? Page { get; set; }

        public int? CreatedByReceptionistID { get; set; }

        public DateTime? CreatedAtFrom { get; set; }

        public DateTime? CreatedAtTo { get; set; }

        // Full constructor
        public PatientFilterDTO(
            string? sort,
            bool? order,
            int? page,
            int? createdByReceptionistID,
            DateTime? createdAtFrom,
            DateTime? createdAtTo,
            string? firstName = null,
            string? middleName = null,
            string? lastName = null,
            char? gender = null,
            DateTime? birthDateFrom = null,
            DateTime? birthDateTo = null,
            int? countryId = null,
            string? phone = null,
            string? email = null)
            : base(firstName, middleName, lastName, gender, birthDateFrom, birthDateTo, countryId, phone, email)
        {
            Sort = sort;
            Order = order;
            Page = page;
            CreatedByReceptionistID = createdByReceptionistID;
            CreatedAtFrom = createdAtFrom;
            CreatedAtTo = createdAtTo;
        }

        // Parameterless constructor
        public PatientFilterDTO() : base()
        {
            Sort = null;
            Order = null;
            Page = null;
            CreatedByReceptionistID = null;
            CreatedAtFrom = null;
            CreatedAtTo = null;
        }
    }
}