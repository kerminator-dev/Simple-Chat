using Chat.Core.DTOs.Requests;

namespace Chat.WebAPIClientLibrary.Services.Interfaces
{
    internal interface IMessagingManager
    {
        public Task<bool> TrySendMessageAsync(SendMessageRequestDTO sendMessageRequestDTO, string accessToken);
    }
}
