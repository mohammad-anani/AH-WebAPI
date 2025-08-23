namespace AH.Domain.Entities
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {
            Email = "";
            Password = "";
        }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public User(string email)
        {
            Email = email;
            Password = "";
        }
    }
}