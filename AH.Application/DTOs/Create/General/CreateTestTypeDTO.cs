using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreateTestTypeDTO
    {
        [Required(ErrorMessage = "Test type name is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Test type name must be between 10 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        [Range(10, 999, ErrorMessage = "Cost must be between 10 and 999")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "Created by admin ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Created by admin ID must be a positive number")]
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