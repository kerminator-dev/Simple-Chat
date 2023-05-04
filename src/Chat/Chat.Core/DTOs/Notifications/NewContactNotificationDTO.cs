using Chat.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.DTOs.Notifications
{
    public class NewContactNotificationDTO
    {
        public string Username { get; set; }    
        public bool IsOnline { get; set; }
    }
}
