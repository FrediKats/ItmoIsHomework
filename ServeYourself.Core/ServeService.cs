using System.Collections.Generic;
using ServeYourself.Core.VisitablePoints;
using ServeYourself.Core.VisitorInputStream;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core
{
    public class ServeService : IServeService
    {
        private readonly IVisitable _endpoint;
        private readonly IVisitorInputStream _visitorInputStream;
        private readonly IVisitable _shop;

        public ServeService()
        {
            //_visitorInputStream = new DummyVisitorInputStream(ServeConfiguration.DummyInputStreamGenerationPeriod);
            _visitorInputStream = new RandomVisitorInputStream(ServeConfiguration.RandomInputStreamMaxDelay);
            _shop = new DummyShop();
            _endpoint = new ServiceEndpoint();
        }

        public void Iteration()
        {
            _endpoint.GetServedClientList();

            _shop.Invoke();
            List<IVisitor> served = _shop.GetServedClientList();
            served.ForEach(c => _endpoint.AddClient(c, 0));

            List<IVisitor> newClients = _visitorInputStream.GenerateClientStream(ServeConfiguration.DeltaTime);
            newClients.ForEach(c => _shop.AddClient(c, ServeConfiguration.DummyClientTransitionTime));
        }

        public string GetStatistic()
        {
            return _shop.GetStatistic().ToString();
        }

        public List<IVisitable> GetAllVisitableList()
        {
            return new List<IVisitable> {_shop};
        }
    }
}