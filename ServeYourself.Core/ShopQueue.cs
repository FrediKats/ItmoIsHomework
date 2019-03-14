using System.Collections.Generic;
using System.Linq;
using ServeYourself.Core.Abstractions;

namespace ServeYourself.Core
{
    public class ShopQueue
    {
        private List<(IClient client, int time)> _queue;

        public ShopQueue()
        {
            _queue = new List<(IClient, int)>();
        }

        public int Count => _queue.Count;

        public List<IClient> UpdateAndGetClient()
        {
            _queue = _queue
                .Select(e => (e.client, e.time - ServeConfiguration.DeltaTime))
                .ToList();
            IEnumerable<IClient> ready = _queue.Where(e => e.time <= 0).Select(e => e.client);
            _queue = _queue.Where(e => e.time > 0).ToList();
            return ready.ToList();
        }

        public void AddClient(IClient client, int time)
        {
            _queue.Add((client, time));
        }
    }
}