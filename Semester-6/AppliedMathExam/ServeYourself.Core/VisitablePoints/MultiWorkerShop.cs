using System.Collections.Generic;
using ServeYourself.Core.DataContainers;
using ServeYourself.Core.Visitors;
using ServeYourself.Core.Workers;

namespace ServeYourself.Core.VisitablePoints
{
    public class MultiWorkerShop : IVisitable
    {
        private readonly WorkerGroupVisitorHandler _workerList;

        private readonly ShopQueue _visitorList = new ShopQueue();
        private readonly Queue<IVisitor> _realQueue = new Queue<IVisitor>();
        private List<IVisitor> _servedClients = new List<IVisitor>();

        public MultiWorkerShop()
        {
            _workerList = new WorkerGroupVisitorHandler(new List<IWorker>
            {
                new DummyWorker(ServeConfiguration.DummyWorkerTime),
                new DummyWorker(ServeConfiguration.DummyWorkerTime),
            });
        }

        public void AddClient(IVisitor visitor, int time)
        {
            _visitorList.AddClient(visitor, time);
        }

        public void Invoke()
        {
            List<IVisitor> comingClientList = _visitorList.UpdateAndGetClient();
            comingClientList.ForEach(c => _realQueue.Enqueue(c));
            _workerList.Handle(_realQueue);
            _workerList.TimeTick(ServeConfiguration.DummyWorkerTime);
            _servedClients.AddRange(_workerList.GetHandledVisitors());
        }

        public List<IVisitor> GetServedClientList()
        {
            List<IVisitor> result = _servedClients;
            _servedClients = new List<IVisitor>();
            return result;
        }

        public ShopStatistic GetStatistic()
        {
            return new ShopStatistic(_visitorList.Count, _realQueue.Count, _workerList.ClientInHandling());
        }
    }
}