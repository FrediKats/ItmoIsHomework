using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSpark.Core
{
    public interface INotificationClient
    {
        Task ReceiveNewMessage(TripletDto message);
    }
    
    public class NotificationHandler : INotificationClient, IAsyncDisposable
    {
        public delegate Task NewMessageHandler(TripletDto newMessage);

        public static readonly Lazy<NotificationHandler> Instance =
            new Lazy<NotificationHandler>(() => new NotificationHandler());

        private HubConnection _connection;

        private NotificationHandler()
        {
        }

        public async Task ReceiveNewMessage(TripletDto message)
        {
            NewMessageHandler handler = NewMessageReceive;

            if (handler != null)
                await handler(message);
        }

        public event NewMessageHandler NewMessageReceive;

        public async Task StartHandling(Guid id)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:51798/Notifications?userid=" + id)
                .AddJsonProtocol(options => options.PayloadSerializerOptions.PropertyNamingPolicy = null)
                .WithAutomaticReconnect()
                .Build();

            _connection.On<TripletDto>(nameof(ReceiveNewMessage), ReceiveNewMessage);

            await _connection.StartAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _connection.DisposeAsync();
        }
    }
}