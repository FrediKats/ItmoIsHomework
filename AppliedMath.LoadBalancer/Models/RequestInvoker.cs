using System.Collections.Generic;
using System.Threading;

namespace AppliedMath.LoadBalancer.Models
{
    public class RequestInvoker
    {
        public RequestInvoker(Logger logger, int workerId)
        {
            _logger = logger;
            _workerId = workerId;
        }

        public void Add(RequestModel request)
        {
            lock (_queue)
            {
                _queue.Enqueue(request);
                _resetEvent.Set();
            }
        }

        public void Start()
        {
            var thread = new Thread(RunInternal) {Name = $"Worker {_workerId}"};
            thread.Start();
        }

        private void RunInternal()
        {
            while (true)
            {
                _resetEvent.WaitOne();
                RequestModel request = null;
                lock (_queue)
                {
                    if (_queue.Count != 0)
                        request = _queue.Dequeue();
                }

                if (request != null)
                {
                    request.Execute();
                    _logger.AddLog($"[{_workerId}] {request}");
                }
            }
        }

        private readonly Queue<RequestModel> _queue = new Queue<RequestModel>();
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private readonly Logger _logger;
        private readonly int _workerId;
    }
}