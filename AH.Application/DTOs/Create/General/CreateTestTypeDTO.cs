using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateTestTypeDTO
    {
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public int Cost { get; set; }
        public int CreatedByAdminID { get; set; }

        public CreateTestTypeDTO()
        {
            Name = string.Empty;
            DepartmentID = -1;
            Cost = 0;
            CreatedByAdminID = -1;
        }

        public CreateTestTypeDTO(string name, int departmentID, int cost, int createdByAdminID)
        {
            Name = name;
            DepartmentID = departmentID;
            Cost = cost;
            CreatedByAdminID = createdByAdminID;
        }

        public TestType ToTestType()
        {
            return new TestType(
                Name,
                new Department(DepartmentID),
                Cost,
                new AdminAudit(CreatedByAdminID)
            );
        }
    }
}