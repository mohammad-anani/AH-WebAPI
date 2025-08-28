using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreatePatientDTO : CreatePersonDTO
    {
        public int CreatedByReceptionistID { get; set; }

        public Patient ToPatient()
        {
            return new Patient(base.ToPerson(), new Receptionist(CreatedByReceptionistID));
        }
    }
}