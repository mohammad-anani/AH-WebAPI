using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdatePatientDTO : UpdatePersonDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Patient ID is required")]
        public int ID { get; set; }

        public UpdatePatientDTO() : base()
        {
            ID = -1;
        }

        public Patient ToPatient()
        {
            return new Patient(ID, base.ToPerson(), new Receptionist(-1));
        }
    }
}