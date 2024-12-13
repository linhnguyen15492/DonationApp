using DonationApp.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DonationApp.API.Hubs
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task PushNotificationsAsync(List<string> messages)
        {
            await Clients.All.PushNotificationsAsync(messages);
        }

        public async Task PushNotificationAsync(string message)
        {
            await Clients.All.PushNotificationAsync(message);
        }
    }
}
