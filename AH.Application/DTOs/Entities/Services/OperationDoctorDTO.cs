using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Entities.Services
{
    public class OperationDoctorDTO
    {
        public int DoctorID { get; set; }

        public string Role { get; set; }

        public OperationDoctorDTO(int doctorID, string role)
        {
            DoctorID = doctorID;
            Role = role;
        }
    }
}