using System.Collections.Generic;
using System.Linq;
using AppliedMath.LoadBalancer.Models;
using AppliedMath.LoadBalancer.Tools;

namespace AppliedMath.LoadBalancer.Services
{
    public class BalancerService
    {
        public BalancerService(LoggerService loggerService)
        {
            _invokerList = Enumerable.Range(1, Config.BalancerHandlersCount).Select(id => new TaskHandler(loggerService, id)).ToList();
        }

        public void Add(RequestModel request)
        {
            _invokerList[_selectedWorker].Add(request);
            _selectedWorker = (_selectedWorker + 1) % Config.BalancerHandlersCount;
        }

        public void Start()
        {
            _invokerList.ForEach(invoker => invoker.Start());
        }

        public List<int> GetSizes()
        {
            return _invokerList.Select(i => i.GetQueueSize()).ToList();
        }

        private readonly List<TaskHandler> _invokerList;
        private int _selectedWorker;
    }
}