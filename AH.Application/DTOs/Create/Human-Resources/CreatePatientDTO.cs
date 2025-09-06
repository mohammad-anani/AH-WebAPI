using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreatePatientDTO : CreatePersonDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Created by receptionist ID is required")]
        public int CreatedByReceptionistID { get; set; }

        public Patient ToPatient()
        {
            return new Patient(base.ToPerson(), new Receptionist(CreatedByReceptionistID));
        }
    }
}