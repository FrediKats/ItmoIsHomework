using System.Collections.Generic;
using ServeYourself.Core.Abstractions;
using ServeYourself.Core.DataContainers;

namespace ServeYourself.Core.DummyImplementation
{
    public class ServiceEndpoint : IVisitable
    {
        private List<IClient> _clients = new List<IClient>();

        public void AddClient(IClient client, int time)
        {
            _clients.Add(client);
        }

        public void Invoke()
        {
        }

        public List<IClient> GetServedClientList()
        {
            List<IClient> clientList = _clients;
            _clients = new List<IClient>();
            return clientList;
        }

        public ShopStatistic GetStatistic()
        {
            throw new System.NotImplementedException();
        }
    }
}