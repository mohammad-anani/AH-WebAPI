using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class Country
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public Country()
        {
            ID = null;
            Name = "";
        }

        public Country(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Country(string name)
        {
            ID = null;
            Name = name;
        }
    }
}
