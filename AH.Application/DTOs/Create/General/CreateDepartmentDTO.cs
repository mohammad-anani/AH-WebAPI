using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateDepartmentDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
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