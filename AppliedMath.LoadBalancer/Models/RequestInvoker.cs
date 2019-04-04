using System;
using System.Collections.Generic;
using System.Threading;

namespace AppliedMath.LoadBalancer.Models
{
    public class RequestInvoker
    {
        private readonly Queue<RequestModel> _queue = new Queue<RequestModel>();
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private readonly Logger _logger;

        public RequestInvoker(Logger logger)
        {
            _logger = logger;
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
            var thread = new Thread(RunInternal);
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
                    _logger.AddLog($"Run {request.TaskId} on {Thread.CurrentThread.ManagedThreadId}");
                    request.Execute();
                }
            }
        }
    }
}