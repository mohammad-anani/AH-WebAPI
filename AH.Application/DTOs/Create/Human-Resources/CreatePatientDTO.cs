using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Create
{
    public class CreatePatientDTO : CreatePersonDTO
    {
        [Required(ErrorMessage = "Created by receptionist ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Created by receptionist ID must be a positive number")]
        public int CreatedByReceptionistID { get; set; }

        public Patient ToPatient()
        {
            return new Patient(base.ToPerson(), new Receptionist(CreatedByReceptionistID));
        }
    }
}