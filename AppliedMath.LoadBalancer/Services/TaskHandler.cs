using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using AppliedMath.LoadBalancer.Models;

namespace AppliedMath.LoadBalancer.Services
{
    public class TaskHandler
    {
        private readonly LoggerService _loggerService;
        private readonly Stopwatch _onExecuting = new Stopwatch();
        private readonly Stopwatch _onTaskWaiting = new Stopwatch();

        private readonly Queue<RequestModel> _queue = new Queue<RequestModel>();
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private readonly int _workerId;
        private bool _isExecuting;


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

        public HandlerStateInfo GetStateInfo()
        {
            lock (_queue)
            {
                int size = _queue.Count;
                if (_isExecuting)
                {
                    size++;
                }

                return new HandlerStateInfo(_workerId, size, _onTaskWaiting.ElapsedMilliseconds,
                    _onExecuting.ElapsedMilliseconds);
            }
        }

        private void RunInternal()
        {
            while (true)
            {
                _onTaskWaiting.Start();
                _resetEvent.WaitOne();
                _onTaskWaiting.Stop();

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
                    _isExecuting = true;
                    _onExecuting.Start();
                    request.Execute();
                    _onExecuting.Stop();
                    _loggerService.AddLog($"[{_workerId}] {request}");
                    _isExecuting = false;
                }
            }
        }
    }
}