using System.Diagnostics;
using System.Threading;
using AppliedMath.LoadBalancer.Tools;

namespace AppliedMath.LoadBalancer.Models
{
    public class RequestModel
    {
        public static RequestModel Create()
        {
            return new RequestModel(Config.Random.Next(1, Config.MaxTaskExecutionTime));
        }

        public void Execute()
        {
            _waitingStopwatch.Stop();
            Thread.Sleep(_time);
        }

        public override string ToString()
        {
            return $"Task-{_taskId} was executed after {_waitingStopwatch.Elapsed:g}";
        }

        private RequestModel(int time)
        {
            _taskId = _globalId;
            _time = time;
            _globalId++;

            _waitingStopwatch.Start();
        }

        private static int _globalId;
        private readonly int _taskId;
        private readonly int _time;
        private readonly Stopwatch _waitingStopwatch = new Stopwatch();
    }
}