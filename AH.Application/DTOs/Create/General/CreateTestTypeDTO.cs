using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateTestTypeDTO : TestTypeFormDTO
    {
        [BindNever]
        public int CreatedByAdminID { get; set; }

        public CreateTestTypeDTO()
        {
            CreatedByAdminID = -1;
        }

        public CreateTestTypeDTO(string name, int departmentID, int cost, int createdByAdminID)
        {
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