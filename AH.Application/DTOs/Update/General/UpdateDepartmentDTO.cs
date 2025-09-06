using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateDepartmentDTO : DepartmentFormDTO
    {
        [Required(ErrorMessage = "Department ID is required")]
        [BindNever]
        public int ID { get; set; }

        public UpdateDepartmentDTO()
        {
            ID = -1;
        }

        public UpdateDepartmentDTO(string name, string phone, int createdByAdminID)
        {
        }

        public Department ToDepartment()
        {
            return new Department(
                ID,
                Name,
                Phone,
                new AdminAudit(-1)
            );
        }
    }
}