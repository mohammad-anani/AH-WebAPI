using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateDepartmentDTO
    {
        [Required(ErrorMessage = "Department name is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Department name must be between 5 and 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Phone must be exactly 8 digits")]
        public string Phone { get; set; }

        [BindNever]
        [Required(ErrorMessage = "Created by admin ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Created by admin ID must be a positive number")]
        public int CreatedByAdminID { get; set; }

        public CreateDepartmentDTO()
        {
            Name = string.Empty;
            Phone = string.Empty;
            CreatedByAdminID = -1;
        }

        public CreateDepartmentDTO(string name, string phone, int createdByAdminID)
        {
            Name = name;
            Phone = phone;
            CreatedByAdminID = createdByAdminID;
        }

        public Department ToDepartment()
        {
            return new Department(
                Name,
                Phone,
                new AdminAudit(CreatedByAdminID)
            );
        }
    }
}