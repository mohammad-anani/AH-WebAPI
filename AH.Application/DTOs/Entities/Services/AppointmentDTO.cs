using AH.Application.DTOs.Row;

namespace AH.Application.DTOs.Entities
{
    public class AppointmentDTO
    {
        public int ID { get; set; }
        public AppointmentRowDTO? PreviousAppointment { get; set; }
        public DoctorRowDTO Doctor { get; set; }
        public ServiceDTO Service { get; set; }

        public AppointmentDTO()
        {
            ID = -1;
            PreviousAppointment = new AppointmentRowDTO();
            Doctor = new DoctorRowDTO();
            Service = new ServiceDTO();
        }

        public AppointmentDTO(int id, AppointmentRowDTO? previousAppointment, DoctorRowDTO doctor, ServiceDTO service)
        {
            ID = id;
            PreviousAppointment = previousAppointment;
            Doctor = doctor;
            Service = service;
        }
    }
}