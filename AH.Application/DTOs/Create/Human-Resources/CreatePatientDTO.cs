using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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