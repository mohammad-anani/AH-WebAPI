using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateReceptionistDTO : UpdateEmployeeDTO
    {
        [Required(ErrorMessage = "Receptionist ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Receptionist ID must be a positive number")]
        public new int ID { get; set; }

        public UpdateReceptionistDTO() : base()
        {
            ID = -1;
        }

        public Receptionist ToReceptionist()
        {
            return new Receptionist(base.ToEmployee());
        }
    }
}