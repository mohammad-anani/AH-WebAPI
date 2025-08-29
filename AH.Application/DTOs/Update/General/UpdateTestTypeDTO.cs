using AH.Domain.Entities;
using AH.Domain.Entities.Audit;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateTestTypeDTO
    {
        [Required(ErrorMessage = "Test type ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Test type ID must be a positive number")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Test type name is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Test type name must be between 10 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        [Range(10, 999, ErrorMessage = "Cost must be between 10 and 999")]
        public int Cost { get; set; }

        public UpdateTestTypeDTO()
        {
            ID = -1;
            Name = string.Empty;
            DepartmentID = -1;
            Cost = 0;
        }

        public UpdateTestTypeDTO(string name, int departmentID, int cost, int createdByAdminID)
        {
            Name = name;
            DepartmentID = departmentID;
            Cost = cost;
        }

        public TestType ToTestType()
        {
            return new TestType(
                Name,
                new Department(DepartmentID),
                Cost,
                new AdminAudit(-1)
            );
        }
    }
}