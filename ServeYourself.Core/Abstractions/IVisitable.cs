using System.Collections.Generic;
using ServeYourself.Core.DataContainers;

namespace ServeYourself.Core.Abstractions
{
    public interface IVisitable
    {
        void AddClient(IClient client, int time);
        void Invoke();
        List<IClient> GetServedClientList();

        ShopStatistic GetStatistic();
    }
}