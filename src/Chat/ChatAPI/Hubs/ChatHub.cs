using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {

        public ChatHub()
        {
            
        }

        public async override Task OnConnectedAsync()
        {
            string username = Context.UserIdentifier;

            if (string.IsNullOrEmpty(username))
                return;

            // Уведомление остальных участников о том, что пользователь вошёл в сеть
            await Clients.Others.SendAsync("UserGetsOnline", Context.UserIdentifier);

            await base.OnConnectedAsync();
        }

        public async override  Task OnDisconnectedAsync(Exception? exception)
        {
            // Уведомление остальных участников о том, что пользователь вышел из сети
            await Clients.Others.SendAsync("UserGetsOffline", Context.UserIdentifier);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
