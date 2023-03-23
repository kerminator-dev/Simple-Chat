using Chat.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Chat.Client.WPF.Entities
{
    internal class UserEntity
    {
        [Key]
        public string Username { get; private set; }
        public OnlineStatus OnlineStatus { get; set; }

        public UserEntity(string username)
        {
            Username = username;
        }
    }
}
