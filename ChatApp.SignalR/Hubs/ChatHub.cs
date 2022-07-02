using ChatApp.SignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) =>
            await Clients.All.SendAsync("receiveMessage", message);
    }
}
