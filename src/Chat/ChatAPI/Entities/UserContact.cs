using ChatAPI.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.WebAPI.Entities
{
    public class UserContact
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Username")]
        public string Username { get; set; }
        public User User { get; set; }

        [ForeignKey("ContactUsername")]
        public string ContactUsername { get; set; }
        public User Contact { get; set; }
    }
}
