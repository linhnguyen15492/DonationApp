using DonationApp.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DonationApp.API.Hubs
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task PushNotificationAsync(List<string> message)
        {
            await Clients.All.PushNotificationAsync(message);
        }
    }
}
