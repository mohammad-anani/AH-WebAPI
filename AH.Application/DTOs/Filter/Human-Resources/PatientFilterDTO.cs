using AH.Application.DTOs.Filter.Helpers;

namespace AH.Application.DTOs.Filter
{
    public class PatientFilterDTO : PersonFilter, IFilterable, IReceptionistAudit
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
            int? countryID = null,
            string? phone = null,
            string? email = null)
            : base(firstName, middleName, lastName, gender, birthDateFrom, birthDateTo, countryID, phone, email)
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