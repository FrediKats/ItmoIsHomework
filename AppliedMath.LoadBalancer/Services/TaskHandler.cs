using System.Collections.Generic;
using System.Threading;
using AppliedMath.LoadBalancer.Models;

namespace AppliedMath.LoadBalancer.Services
{
    public class TaskHandler
    {
        private readonly LoggerService _loggerService;

        private readonly Queue<RequestModel> _queue = new Queue<RequestModel>();
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private readonly int _workerId;

        public TaskHandler(LoggerService loggerService, int workerId)
        {
            _loggerService = loggerService;
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

        public int GetQueueSize()
        {
            lock (_queue)
            {
                return _queue.Count;
            }
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
                    {
                        request = _queue.Dequeue();
                    }
                }

                if (request != null)
                {
                    request.Execute();
                    _loggerService.AddLog($"[{_workerId}] {request}");
                }
            }
        }
    }
}