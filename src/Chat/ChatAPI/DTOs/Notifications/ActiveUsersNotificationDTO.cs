using Newtonsoft.Json;

namespace ChatAPI.DTOs.Notifications
{
    public class ActiveUsersNotificationDTO
    {
        [JsonProperty("activeUsers")]
        public IEnumerable<string> Usernames { get; set;}
    }
}
