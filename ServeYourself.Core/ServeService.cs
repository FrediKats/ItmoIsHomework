using System.Collections.Generic;
using ServeYourself.Core.Abstractions;
using ServeYourself.Core.DummyImplementation;

namespace ServeYourself.Core
{
    public class ServeService
    {
        private readonly IInputStream _inputStream;
        public readonly IVisitable Shop;
        private readonly IVisitable _endpoint;

        public ServeService()
        {
            //_inputStream = new DummyInputStream(ServeConfiguration.DummyInputStreamGenerationPeriod);
            _inputStream = new RandomInputStream(ServeConfiguration.RandomInputStreamMaxDelay);
            Shop = new DummyShop();
            _endpoint = new ServiceEndpoint();
        }

        public void Iteration()
        {
            _endpoint.GetServedClientList();

            Shop.Invoke();
            List<IClient> served = Shop.GetServedClientList();
            served.ForEach(c => _endpoint.AddClient(c, 0));

            List<IClient> newClients = _inputStream.GenerateClientStream(ServeConfiguration.DeltaTime);
            newClients.ForEach(c => Shop.AddClient(c, ServeConfiguration.DummyClientTransitionTime));
        }

        public string GetStatistic()
        {
            return Shop.GetStatistic().ToString();
        }
    }
}