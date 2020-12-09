using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SmartSpark.Core;

namespace SmartSpark.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Select role: ");
            var role = SelectRole(Console.ReadLine());
            var speechReader = new SpeechReader(SpeechLoader.GetAll(), role);


            using (var rdfClient = new RdfClient(role, speechReader))
            {
                rdfClient.IsEnd.WaitOne();
            }
        }

        private static string SelectRole(string index)
        {
            if (index == "1")
                return "Бернардо";

            if (index == "2")
                return "Франциско";

            if (index == "3")
                return "Горацио";

            return "Марцелл";
        }
    }

    public class RdfClient : IDisposable
    {
        public ManualResetEvent IsEnd = new ManualResetEvent(false);
        private static HttpClient _httpClient;
        private SpeechReader _reader;
        
        public RdfClient(string name, SpeechReader reader)
        {
            _httpClient = new HttpClient();
            Name = name;
            _reader = reader;
            NotificationHandler.Instance.Value.StartHandling(Guid.NewGuid()).Wait();
            NotificationHandler.Instance.Value.NewMessageReceive += OnMessage;
            
            var speech = _reader.TrySayFirst();
            if (speech is not null)
                Say(speech.Content);
        }

        public String Name { get; set; }

        public void Send(string predicate, string message)
        {
            _httpClient.GetAsync($"http://localhost:51798/Rdf/create?subject={Name}&predicate={predicate}&obj={HttpUtility.UrlEncode(message)}");
        }

        public void Say(string message)
        {
            Console.WriteLine($"[{Name}] heard: {message}");
            _httpClient.GetAsync($"http://localhost:51798/Rdf/create?subject={Name}&predicate=say&obj={HttpUtility.UrlEncode(message)}");
        }

        public Task OnMessage(TripletDto message)
        {
            Thread.Sleep(1000);

            if (message.Subject == Name || message.Predicate != "say")
                return Task.CompletedTask;
            
            Console.WriteLine($"[{Name}] heard: {message.Object}");
            var speech = _reader.TryContinue(message.Object);
            if (speech is not null)
            {
                Say(speech.Content);
            }
            else if (_reader.IsEnd(message.Object))
            {
                IsEnd.Set();
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Send("leave", "this");
            NotificationHandler.Instance.Value.NewMessageReceive -= OnMessage;
        }
    }
}
