namespace AH.Domain.Entities
{
    public class Appointment
    {
        public int ID { get; set; }
        public PreviousAppointment? PreviousAppointment { get; set; }
        public Doctor Doctor { get; set; }
        public Service Service { get; set; }

        public Appointment()
        {
            ID = -1;
            PreviousAppointment = new PreviousAppointment();
            Doctor = new Doctor();
            Service = new Service();
        }

        public Appointment(int id, PreviousAppointment? previousAppointment, Doctor doctor, Service service)
        {
            ID = id;
            PreviousAppointment = previousAppointment;
            Doctor = doctor;
            Service = service;
        }

        public Appointment(int id)
        {
            ID = id;
            PreviousAppointment = null;
            Doctor = new Doctor();
            Service = new Service();
        }

        public Appointment(PreviousAppointment? previousAppointment, Doctor doctor, Service service)
        {
            ID = -1;
            PreviousAppointment = previousAppointment;
            Doctor = doctor;
            Service = service;
        }
    }
}