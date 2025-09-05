using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateDepartmentDTO : DepartmentFormDTO
    {
        [BindNever]
        public int CreatedByAdminID { get; set; }

        public CreateDepartmentDTO()
        {
            CreatedByAdminID = -1;
        }

        public CreateDepartmentDTO(string name, string phone, int createdByAdminID)
        {
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