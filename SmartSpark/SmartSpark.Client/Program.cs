using System;
using System.Net.Http;
using System.Threading.Tasks;
using SmartSpark.Core;

namespace SmartSpark.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var rdfClient = new RdfClient())
            {
                Console.WriteLine($"[{rdfClient.Tag}] Start");

                NotificationHandler.Instance.Value.NewMessageReceive += rdfClient.OnMessage;
                while (true)
                {
                    var readLine = Console.ReadLine();
                    if (string.IsNullOrEmpty(readLine))
                        return;
                
                    rdfClient.Send(readLine);
                }
            }
        }
    }

    public class RdfClient : IDisposable
    {
        private static HttpClient _httpClient;

        public RdfClient()
        {
            _httpClient = new HttpClient();
            Id = Guid.NewGuid();
            NotificationHandler.Instance.Value.StartHandling(Id).Wait();
            NotificationHandler.Instance.Value.NewMessageReceive += OnMessage;
        }

        public Guid Id { get; set; }
        public String Tag => Id.ToString().Substring(0, 4);

        public void Send(string message)
        {
            _httpClient.GetAsync($"http://localhost:51798/Rdf/create?subject={Tag}&predicate=say&obj={message}");
        }

        public Task OnMessage(TripletDto message)
        {
            Console.WriteLine($"[{Tag}] New message get: {message}");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            NotificationHandler.Instance.Value.NewMessageReceive -= OnMessage;
        }
    }
}
