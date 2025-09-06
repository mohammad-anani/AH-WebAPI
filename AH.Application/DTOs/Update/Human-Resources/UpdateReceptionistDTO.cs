using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateReceptionistDTO : UpdateEmployeeDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Receptionist ID is required")]
        public new int ID { get; set; }

        public UpdateReceptionistDTO() : base()
        {
            ID = -1;
        }

        public Receptionist ToReceptionist()
        {
            return new Receptionist(ID, base.ToEmployee());
        }
    }
}