using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.DTOs.Entities
{
    public class PatientDTO
    {
        public int ID { get; set; }
        public Person Person { get; set; }
        public ReceptionistRowDTO CreatedByReceptionist { get; set; }
        public DateTime CreatedAt { get; set; }

        public PatientDTO()
        {
            ID = -1;
            Person = new Person();
            CreatedByReceptionist = new ReceptionistRowDTO();
            CreatedAt = DateTime.MinValue;
        }

        public PatientDTO(int id, Person person, ReceptionistRowDTO createdByReceptionist, DateTime createdAt)
        {
            ID = id;
            Person = person;
            CreatedByReceptionist = createdByReceptionist;
            CreatedAt = createdAt;
        }
    }
}