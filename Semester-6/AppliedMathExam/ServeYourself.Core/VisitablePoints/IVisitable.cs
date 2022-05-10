using System.Collections.Generic;
using ServeYourself.Core.DataContainers;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.VisitablePoints
{
    public interface IVisitable
    {
        void AddClient(IVisitor visitor, int time);
        void Invoke();
        List<IVisitor> GetServedClientList();

        ShopStatistic GetStatistic();
    }
}