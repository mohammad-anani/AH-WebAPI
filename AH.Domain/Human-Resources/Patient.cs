namespace AH.Domain.Entities
{
    public class Patient
    {
        public int ID { get; set; }
        public Person Person { get; set; }
        public Receptionist CreatedByReceptionist { get; set; }
        public DateTime CreatedAt { get; set; }

        public Patient()
        {
            ID = -1;
            Person = new Person();
            CreatedByReceptionist = new Receptionist(); // Fix: Don't create new Receptionist to avoid circular dependency
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
            ID = -1;
            Person = person;
            CreatedByReceptionist = createdByReceptionist;
            CreatedAt = DateTime.MinValue;
        }
    }
}