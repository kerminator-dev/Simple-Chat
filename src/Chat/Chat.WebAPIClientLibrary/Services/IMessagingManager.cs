using Chat.Core.DTOs.Requests;

namespace Chat.WebAPIClientLibrary.Services
{
    public interface IMessagingManager
    {
        public Task<bool> TrySendMessage(SendMessageRequestDTO sendMessageRequestDTO, string accessToken);
    }
}
