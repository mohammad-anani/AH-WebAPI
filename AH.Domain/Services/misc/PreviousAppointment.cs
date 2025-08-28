namespace AH.Domain.Entities
{
    public class PreviousAppointment
    {
        public int ID { get; set; }
        public int? PreviousAppointmentID { get; set; }
        public Doctor Doctor { get; set; }
        public Service Service { get; set; }

        public PreviousAppointment()
        {
            ID = -1;
            PreviousAppointmentID = null;
            Doctor = new Doctor();
            Service = new Service();
        }

        public PreviousAppointment(int id, int? previousAppointmentID, Doctor doctor, Service service)
        {
            ID = id;
            PreviousAppointmentID = previousAppointmentID;
            Doctor = doctor;
            Service = service;
        }

        public PreviousAppointment(int id)
        {
            ID = id;
            PreviousAppointmentID = null;
            Doctor = new Doctor();
            Service = new Service();
        }

        public PreviousAppointment(int? previousAppointmentID, Doctor doctor, Service service)
        {
            ID = -1;
            PreviousAppointmentID = previousAppointmentID;
            Doctor = doctor;
            Service = service;
        }
    }
}