using System.Collections.ObjectModel;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Url> Urls { get; set; }

        public User()
        {
            Urls = new Collection<Url>();
        }
    }
}