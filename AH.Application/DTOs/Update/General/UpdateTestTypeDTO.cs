using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateTestTypeDTO : TestTypeFormDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Test type ID is required")]
        public int ID { get; set; }

        public UpdateTestTypeDTO()
        {
            ID = -1;
        }

        public UpdateTestTypeDTO(string name, int departmentID, int cost, int createdByAdminID)
        {
        }

        public TestType ToTestType()
        {
            return new TestType(
                ID,
                Name,
                new Department(DepartmentID),
                Cost,
                new AdminAudit(-1)
            );
        }
    }
}