﻿using ProjectManager.Application.Hubs;
using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    public class NotificationService : INotificationService
    {
        public Task NotifyAsync(int count, string nameIdentifier)
        {
            NotificationHub.SendNotification(count, nameIdentifier);
            return Task.CompletedTask;
        }
    }
}
