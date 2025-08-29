using AH.Domain.Entities;

namespace AH.Application.DTOs.Create
{
    public class CreateAdminDTO : CreateEmployeeDTO
    {
        public CreateAdminDTO() : base()
        {
        }

        public Admin ToAdmin()
        {
            return new Admin(base.ToEmployee());
        }
    }
}