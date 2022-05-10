using System.Collections.Generic;
using ServeYourself.Core.VisitablePoints;

namespace ServeYourself.Core.ServeServices
{
    public interface IServeService
    {
        void Iteration();
        string GetStatistic();

        List<IVisitable> GetAllVisitableList();
    }
}