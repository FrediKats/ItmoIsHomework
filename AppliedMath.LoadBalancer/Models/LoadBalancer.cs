namespace AppliedMath.LoadBalancer.Models
{
    public class LoadBalancer
    {
        private readonly RequestInvoker _invoker;

        public LoadBalancer(Logger logger)
        {
            _invoker = new RequestInvoker(logger);
        }

        public void Add(RequestModel request)
        {
            _invoker.Add(request);
        }

        public void Start()
        {
            _invoker.Start();
        }
    }
}