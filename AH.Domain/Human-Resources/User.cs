using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Domain.Entities
{
    public class User
    {
        public int? ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {
            ID = null;
            Email = "";
            Password = "";
        }

        public User(int id, string email, string password)
        {
            ID = id;
            Email = email;
            Password = password;
        }

        public User(string email, string password)
        {
            ID = null;
            Email = email;
            Password = password;
        }
    }
}
