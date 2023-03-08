using Newtonsoft.Json;

namespace Chat.Core.DTOs.Notifications
{
    public class ActiveUsersNotificationDTO
    {
        [JsonProperty("activeUsers")]
        public IEnumerable<string> Usernames { get; set;}
    }
}
