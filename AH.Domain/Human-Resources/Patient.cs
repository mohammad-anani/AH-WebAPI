using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Patient
    {
        public int? ID { get; set; }
        public Person Person { get; set; }
        public Receptionist CreatedByReceptionist { get; set; }
        public DateTime CreatedAt { get; set; }

        public Patient()
        {
            ID = null;
            Person = new Person();
            CreatedByReceptionist = null; // Fix: Don't create new Receptionist to avoid circular dependency
            CreatedAt = DateTime.MinValue;
        }

        public Patient(int id, Person person, Receptionist createdByReceptionist, DateTime createdAt)
        {
            ID = id;
            Person = person;
            CreatedByReceptionist = createdByReceptionist;
            CreatedAt = createdAt;
        }

        public Patient(Person person, Receptionist createdByReceptionist)
        {
            ID = null;
            Person = person;
            CreatedByReceptionist = createdByReceptionist;
            CreatedAt = DateTime.MinValue;
        }
    }
}
