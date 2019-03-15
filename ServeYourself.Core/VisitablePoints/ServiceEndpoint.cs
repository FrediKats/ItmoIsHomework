using System.Collections.Generic;
using ServeYourself.Core.DataContainers;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.VisitablePoints
{
    public class ServiceEndpoint : IVisitable
    {
        private List<IVisitor> _clients = new List<IVisitor>();

        public void AddClient(IVisitor visitor, int time)
        {
            _clients.Add(visitor);
        }

        public void Invoke()
        {
        }

        public List<IVisitor> GetServedClientList()
        {
            List<IVisitor> clientList = _clients;
            _clients = new List<IVisitor>();
            return clientList;
        }

        public ShopStatistic GetStatistic()
        {
            throw new System.NotImplementedException();
        }
    }
}