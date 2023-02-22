using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Entities
{
    public class User
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
