using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SmartSpark.Core;

namespace SmartSpark.Server
{
    public class NotificationHub : Hub<INotificationClient>
    {
        private readonly ConnectionManager _connectionManager = new ConnectionManager();

        public override Task OnConnectedAsync()
        {
            HttpContext context = Context.GetHttpContext();
            String id = context.Request.Query["userId"].FirstOrDefault();

            if (id is null || !Guid.TryParse(id, out Guid userId))
                throw new Exception();

            _connectionManager.AddConnection(userId, Context.ConnectionId);

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            HttpContext context = Context.GetHttpContext();
            String id = context.Request.Query["userId"].FirstOrDefault();
            if (id is null || !Guid.TryParse(id, out Guid userId))
                throw new Exception();

            _connectionManager.RemoveConnection(userId, Context.ConnectionId);
            return Task.CompletedTask;
        }
    }
}