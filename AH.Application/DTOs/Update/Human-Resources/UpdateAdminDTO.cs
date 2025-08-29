using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateAdminDTO : UpdateEmployeeDTO
    {
        public Admin ToAdmin()
        {
            return new Admin(base.ToEmployee());
        }
    }
}