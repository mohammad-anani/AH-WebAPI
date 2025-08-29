using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdatePatientDTO : UpdatePersonDTO
    {
        [Required(ErrorMessage = "Patient ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be a positive number")]
        public int ID { get; set; }

        public UpdatePatientDTO() : base()
        {
            ID = -1;
        }

        public Patient ToPatient()
        {
            return new Patient(base.ToPerson(), new Receptionist(-1));
        }
    }
}