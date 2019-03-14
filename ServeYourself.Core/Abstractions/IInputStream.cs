using System.Collections.Generic;

namespace ServeYourself.Core.Abstractions
{
    public interface IInputStream
    {
        List<IClient> GenerateClientStream(int time);
    }
}