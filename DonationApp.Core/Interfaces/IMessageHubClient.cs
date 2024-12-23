﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Interfaces
{
    public interface IMessageHubClient
    {
        Task PushNotificationsAsync(List<string> messages);
        Task PushNotificationAsync(string message);
    }
}
