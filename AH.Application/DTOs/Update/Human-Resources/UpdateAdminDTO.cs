using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateAdminDTO : UpdateEmployeeDTO
    {
        [Required(ErrorMessage = "Admin ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Admin ID must be a positive number")]
        public new int ID { get; set; }

        public UpdateAdminDTO() : base()
        {
            ID = -1;
        }

        public Admin ToAdmin()
        {
            return new Admin(base.ToEmployee());
        }
    }
}