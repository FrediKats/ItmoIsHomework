using System.Collections.Generic;
using System.Linq;

namespace AppliedMath.LoadBalancer.Models
{
    public class LoadBalancer
    {
        public const int Size = 4;
        
        public LoadBalancer(Logger logger)
        {
            _invokerList = Enumerable.Range(1, Size).Select(id => new RequestInvoker(logger, id)).ToList();
        }

        public void Add(RequestModel request)
        {
            _invokerList[_selectedWorker].Add(request);
            _selectedWorker = (_selectedWorker + 1) % Size;
        }

        public void Start()
        {
            _invokerList.ForEach(invoker => invoker.Start());
        }

        public List<int> GetSizes()
        {
            return _invokerList.Select(i => i.GetQueueSize()).ToList();
        }

        private readonly List<RequestInvoker> _invokerList;
        private int _selectedWorker;
    }
}