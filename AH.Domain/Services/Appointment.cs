using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Appointment
    {
        public int? ID { get; set; }
        public int? PreviousAppointmentID { get; set; }
        public Doctor Doctor { get; set; }
        public Service Service { get; set; }

        public Appointment()
        {
            ID = null;
            PreviousAppointmentID = null;
            Doctor = new Doctor();
            Service = new Service();
        }

        public Appointment(int id, int? previousAppointmentID, Doctor doctor, Service service)
        {
            ID = id;
            PreviousAppointmentID = previousAppointmentID;
            Doctor = doctor;
            Service = service;
        }

        public Appointment(int? previousAppointmentID, Doctor doctor, Service service)
        {
            ID = null;
            PreviousAppointmentID = previousAppointmentID;
            Doctor = doctor;
            Service = service;
        }
    }
}
