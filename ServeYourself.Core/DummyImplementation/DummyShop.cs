using System.Collections.Generic;
using ServeYourself.Core.Abstractions;
using ServeYourself.Core.DataContainers;

namespace ServeYourself.Core.DummyImplementation
{
    public class DummyShop : IVisitable
    {
        private readonly ShopQueue _visitorList = new ShopQueue();
        private readonly Queue<IClient> _realQueue = new Queue<IClient>();
        private readonly IWorker _worker = new DummyWorker(ServeConfiguration.DummyWorkerTime);
        private List<IClient> _servedClients = new List<IClient>();

        public void AddClient(IClient client, int time)
        {
            _visitorList.AddClient(client, time);
        }

        public void Invoke()
        {
            List<IClient> comingClientList = _visitorList.UpdateAndGetClient();
            comingClientList.ForEach(c => _realQueue.Enqueue(c));
            if (_worker.IsAvailable() && _realQueue.Count != 0)
            {
                IClient client = _realQueue.Dequeue();
                _worker.AddClient(client);
            }

            _worker.ApplyTime(ServeConfiguration.DeltaTime);

            if (_worker.IsClientServed())
            {
                _servedClients.Add(_worker.GetServedClient());
            }
        }

        public List<IClient> GetServedClientList()
        {
            List<IClient> result = _servedClients;
            _servedClients = new List<IClient>();
            return result;
        }

        public ShopStatistic GetStatistic()
        {
            return new ShopStatistic(_visitorList.Count, _realQueue.Count, _worker.IsAvailable() ? 0 : 1);
        }
    }
}