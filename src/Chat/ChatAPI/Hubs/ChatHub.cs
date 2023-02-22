using ChatAPI.DTOs.Notifications;
using ChatAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class ChatHub : Hub
    {
        [Authorize]
        public async Task Send(string name, string message)
        {
            await Clients.All.SendAsync(name, message);
        }


        [Authorize]
        public override Task OnConnectedAsync()
        {



            return base.OnConnectedAsync();
        }
    }
}
