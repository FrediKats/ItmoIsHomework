using System.Collections.Generic;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.VisitorInputStream
{
    public interface IVisitorInputStream
    {
        List<IVisitor> GenerateClientStream(int time);
    }
}