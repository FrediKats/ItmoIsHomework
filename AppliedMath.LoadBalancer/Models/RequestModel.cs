using System;
using System.Diagnostics;
using System.Threading;
using AppliedMath.LoadBalancer.Tools;

namespace AppliedMath.LoadBalancer.Models
{
    public class RequestModel
    {
        public int TaskId { get; }

        public static RequestModel Create()
        {
            return new RequestModel(Config.Random.Next(1, Config.MaxTaskDelay));
        }

        private RequestModel(int time)
        {
            _time = time;

            TaskId = _globalId;
            _globalId++;
            _stopwatch.Start();
        }

        public void Execute()
        {
            _stopwatch.Stop();
            Thread.Sleep(_time);
        }

        public override string ToString()
        {
            return $"Task-{TaskId} was executed after {_stopwatch.Elapsed:g}";
        }

        private static int _globalId;
        private readonly int _time;
        private readonly Stopwatch _stopwatch = new Stopwatch();
    }
}