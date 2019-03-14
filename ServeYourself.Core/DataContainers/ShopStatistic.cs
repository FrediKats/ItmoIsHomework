namespace ServeYourself.Core.DataContainers
{
    public struct ShopStatistic
    {
        public int VisitorCount { get; }
        public int InQueueClientCount { get; }
        public int HandlingClients { get; }

        public ShopStatistic(int visitorCount, int inQueueClientCount, int handlingClients)
        {
            VisitorCount = visitorCount;
            InQueueClientCount = inQueueClientCount;
            HandlingClients = handlingClients;
        }

        public override string ToString()
        {
            return $"V:{VisitorCount:0000} Q:{InQueueClientCount:0000} H:{HandlingClients:00}";
        }
    }
}