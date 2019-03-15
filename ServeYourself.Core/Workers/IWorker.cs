using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.Workers
{
    public interface IWorker
    {
        bool IsAvailable();
        bool IsClientServed();

        bool AddClient(IVisitor visitor);
        void ApplyTime(int time);
        IVisitor GetServedClient();
    }
}