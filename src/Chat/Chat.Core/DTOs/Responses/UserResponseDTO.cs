using Chat.Core.Enums;

namespace Chat.Core.DTOs.Responses
{
    public class UserResponseDTO
    {
        public string Username { get; private set; }
        public bool IsOnline => _onlineStatus == OnlineStatus.Online;

        private readonly OnlineStatus _onlineStatus;

        public UserResponseDTO(string username, OnlineStatus onlineStatus)
        {
            Username = username;
            _onlineStatus = onlineStatus;
        }
    }
}
