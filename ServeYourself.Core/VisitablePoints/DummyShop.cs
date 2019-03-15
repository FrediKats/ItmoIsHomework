using System.Collections.Generic;
using ServeYourself.Core.DataContainers;
using ServeYourself.Core.Visitors;
using ServeYourself.Core.Workers;

namespace ServeYourself.Core.VisitablePoints
{
    public class DummyShop : IVisitable
    {
        private readonly ShopQueue _visitorList = new ShopQueue();
        private readonly Queue<IVisitor> _realQueue = new Queue<IVisitor>();
        private readonly IWorker _worker = new DummyWorker(ServeConfiguration.DummyWorkerTime);
        private List<IVisitor> _servedClients = new List<IVisitor>();

        public void AddClient(IVisitor visitor, int time)
        {
            _visitorList.AddClient(visitor, time);
        }

        public void Invoke()
        {
            List<IVisitor> comingClientList = _visitorList.UpdateAndGetClient();
            comingClientList.ForEach(c => _realQueue.Enqueue(c));
            if (_worker.IsAvailable() && _realQueue.Count != 0)
            {
                IVisitor visitor = _realQueue.Dequeue();
                _worker.AddClient(visitor);
            }

            _worker.ApplyTime(ServeConfiguration.DeltaTime);

            if (_worker.IsClientServed())
            {
                _servedClients.Add(_worker.GetServedClient());
            }
        }

        public List<IVisitor> GetServedClientList()
        {
            List<IVisitor> result = _servedClients;
            _servedClients = new List<IVisitor>();
            return result;
        }

        public ShopStatistic GetStatistic()
        {
            return new ShopStatistic(_visitorList.Count, _realQueue.Count, _worker.IsAvailable() ? 0 : 1);
        }
    }
}