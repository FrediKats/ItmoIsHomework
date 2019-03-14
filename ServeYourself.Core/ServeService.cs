using System.Collections.Generic;
using ServeYourself.Core.Abstractions;
using ServeYourself.Core.DummyImplementation;

namespace ServeYourself.Core
{
    public class ServeService
    {
        private readonly IInputStream _inputStream;
        private readonly IVisitable _shop;
        private readonly IVisitable _endpoint;

        public ServeService()
        {
            _inputStream = new DummyInputStream(ServeConfiguration.DummyInputStreamGenerationPeriod);
            _shop = new DummyShop();
            _endpoint = new ServiceEndpoint();
        }

        public void Iteration()
        {
            _endpoint.GetServedClientList();

            _shop.Invoke();
            List<IClient> served = _shop.GetServedClientList();
            served.ForEach(c => _endpoint.AddClient(c, 0));

            List<IClient> newClients = _inputStream.GenerateClientStream(ServeConfiguration.DeltaTime);
            newClients.ForEach(c => _shop.AddClient(c, ServeConfiguration.DummyClientTransitionTime));
        }

        public string GetStatistic()
        {
            return _shop.GetStatistic().ToString();
        }
    }
}