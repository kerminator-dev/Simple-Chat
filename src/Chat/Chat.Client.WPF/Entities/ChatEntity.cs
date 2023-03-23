using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chat.Client.WPF.Entities
{
    public class ChatEntity
    {
        [Key]
        public string ContactUsername { get; set; }
        public string ChatPassword { get; set; } 

        public IEnumerable<MessageEntity> Messages { get; set; }
    }
}
