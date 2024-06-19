using Microsoft.AspNet.SignalR;

namespace ProjectManager.Application.Hubs
{
    public class NotificationHub : Hub
    {
        public static void SendNotification(int count)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.updateNotificationCount(count);
        }
    }
}
