namespace ServeYourself.Core.DataContainers
{
    public struct ShopStatistic
    {
        public int InQueueClientCount { get; }
        public int HandlingClients { get; }

        public ShopStatistic(int visitorCount, int inQueueClientCount, int handlingClients)
        {
            InQueueClientCount = inQueueClientCount + visitorCount;
            HandlingClients = handlingClients;
        }

        public override string ToString()
        {
            return $"Q:{InQueueClientCount:0000} H:{HandlingClients:00}";
        }
    }
}