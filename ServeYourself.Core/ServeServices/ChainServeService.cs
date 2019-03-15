using System.Collections.Generic;
using System.Linq;
using ServeYourself.Core.VisitablePoints;
using ServeYourself.Core.VisitorInputStream;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.ServeServices
{
    public class ChainServeService : IServeService
    {
        private readonly IVisitorInputStream _visitorInputStream;
        private readonly List<IVisitable> _shops;
        private readonly IVisitable _endpoint;

        private readonly SimpleVisitorTransitSystem _transit = new SimpleVisitorTransitSystem();

        public ChainServeService()
        {
            _visitorInputStream = new DummyVisitorInputStream(ServeConfiguration.DummyInputStreamGenerationPeriod);
            _shops = new List<IVisitable>()
            {
                new DummyShop(),
                new DummyShop(),
                new DummyShop(),
                new DummyShop(),
            };
            _endpoint = new ServiceEndpoint();
        }

        public void Iteration()
        {
            _endpoint.GetServedClientList();

            foreach (IVisitable visitable in _shops)
            {
                visitable.Invoke();
            }

            foreach (IVisitable visitable in _shops)
            {
                List<IVisitor> servedClientList = visitable.GetServedClientList();
                servedClientList.ForEach(v => _transit
                    .MoveVisitor(v, _shops
                        .Where(s => s != visitable)
                        .Append(_endpoint)
                        .ToList()));
            }

            List<IVisitor> newClients = _visitorInputStream.GenerateClientStream(ServeConfiguration.DeltaTime);
            newClients.ForEach(v => _transit.MoveVisitor(v, _shops.Append(_endpoint).ToList()));
        }

        public string GetStatistic()
        {
            return string.Join("\n", _shops);
        }

        public List<IVisitable> GetAllVisitableList()
        {
            return _shops;
        }
    }
}