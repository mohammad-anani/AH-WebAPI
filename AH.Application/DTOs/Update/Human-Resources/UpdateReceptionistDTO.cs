using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateReceptionistDTO : UpdateEmployeeDTO
    {
        public int ID { get; set; }

        public Receptionist ToReceptionist()
        {
            return new Receptionist(base.ToEmployee());
        }
    }
}