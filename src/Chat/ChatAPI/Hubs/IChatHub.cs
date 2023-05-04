using Chat.Core.DTOs.Responses;
using Microsoft.AspNetCore.SignalR;

namespace Chat.WebAPI.Hubs
{
    public interface IChatHub
    {
        Task NotifyContactAdded(string username, UserContactsResponseDTO contacts);

        Task NotifyContactDeleted(string username, UserContactsResponseDTO contacts);

        Task SendAsync(string method, object arg1, CancellationToken cancellationToken = default);
    }
}
