namespace AH.Domain.Entities
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Country()
        {
            ID = -1;
            Name = "";
        }

        public Country(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Country(int id)
        {
            ID = id;
            Name = "";
        }
    }
}