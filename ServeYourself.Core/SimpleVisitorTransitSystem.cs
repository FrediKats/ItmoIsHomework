using System.Collections.Generic;
using ServeYourself.Core.Tools;
using ServeYourself.Core.VisitablePoints;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core
{
    public class SimpleVisitorTransitSystem
    {
        public void MoveVisitor(IVisitor visitor, List<IVisitable> visitableList)
        {
            int index = RandomProvider.Random.Next(visitableList.Count);

            visitableList[index].AddClient(visitor, 0);
        }
    }
}