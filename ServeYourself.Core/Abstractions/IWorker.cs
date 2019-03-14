namespace ServeYourself.Core.Abstractions
{
    public interface IWorker
    {
        bool IsAvailable();
        bool IsClientServed();

        bool AddClient(IClient client);
        void ApplyTime(int time);
        IClient GetServedClient();
    }
}