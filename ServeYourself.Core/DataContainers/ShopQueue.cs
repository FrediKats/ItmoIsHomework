using System.Collections.Generic;
using System.Linq;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.DataContainers
{
    public class ShopQueue
    {
        private List<(IVisitor client, int time)> _queue;

        public ShopQueue()
        {
            _queue = new List<(IVisitor, int)>();
        }

        public int Count => _queue.Count;

        public List<IVisitor> UpdateAndGetClient()
        {
            _queue = _queue
                .Select(e => (e.client, e.time - ServeConfiguration.DeltaTime))
                .ToList();
            IEnumerable<IVisitor> ready = _queue.Where(e => e.time <= 0).Select(e => e.client);
            _queue = _queue.Where(e => e.time > 0).ToList();
            return ready.ToList();
        }

        public void AddClient(IVisitor visitor, int time)
        {
            _queue.Add((visitor, time));
        }
    }
}