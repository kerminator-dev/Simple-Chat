using Chat.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Entities
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public virtual ICollection<UserContact> Contacts { get; set;}
    }
}
