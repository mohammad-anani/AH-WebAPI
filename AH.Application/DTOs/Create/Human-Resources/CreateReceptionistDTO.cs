using AH.Domain.Entities;

namespace AH.Application.DTOs.Create
{
    public class CreateReceptionistDTO : CreateEmployeeDTO
    {
        public Receptionist ToReceptionist()
        {
            return new Receptionist(base.ToEmployee());
        }
    }
}