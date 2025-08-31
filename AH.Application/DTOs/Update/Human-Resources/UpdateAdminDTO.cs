using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateAdminDTO : UpdateEmployeeDTO
    {
        [BindNever]
        public new int ID { get; set; }

        public UpdateAdminDTO() : base()
        {
        }

        public Admin ToAdmin()
        {
            return new Admin(ID, base.ToEmployee());
        }
    }
}