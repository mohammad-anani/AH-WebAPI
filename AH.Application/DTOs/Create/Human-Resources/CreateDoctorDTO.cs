using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class CreateDoctorDTO : CreateEmployeeDTO
    {
        public string Specialization { get; set; }

        public int CostPerAppointment { get; set; }

        public Doctor ToDoctor()
        {
            return new Doctor(base.ToEmployee()
            ,
                 CostPerAppointment,
                Specialization
            );
        }
    }
}