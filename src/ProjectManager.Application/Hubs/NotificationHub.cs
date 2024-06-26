using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Application.Hubs
{
    public class NotificationHub : Hub
    {

        private static Dictionary<string, string> _connections = new Dictionary<string, string>();

        public override Task OnConnected()
        {

            string nameIdentifier = Context.QueryString["nameIdentifier"];
                _connections[Context.ConnectionId] = nameIdentifier;

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            string nameIdentifier = Context.QueryString["nameIdentifier"];
            if (!_connections.ContainsKey(Context.ConnectionId))
            {
                _connections[Context.ConnectionId] = nameIdentifier;
            }

            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _connections.Remove(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public static void SendNotification(int count, string nameIdentifier)
        {
            var connectionId = _connections.FirstOrDefault(x => x.Value == nameIdentifier).Key;
            if (connectionId != null)
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                context.Clients.Client(connectionId).updateNotificationCount(count);
            }
        }
    }
}
