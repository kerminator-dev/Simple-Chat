using Chat.Core.DTOs.Requests;

namespace Chat.WebAPIClientLibrary.Services.Interfaces
{
    internal interface IMessagingManager
    {
        public Task<bool> TrySendMessageAsync(SendTextMessageRequestDTO sendMessageRequestDTO, string accessToken);
    }
}
