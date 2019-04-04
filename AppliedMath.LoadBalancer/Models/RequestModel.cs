using System;
using System.Threading;

namespace AppliedMath.LoadBalancer.Models
{
    public class RequestModel
    {
        public int TaskId { get; }

        public static RequestModel Create()
        {
            return new RequestModel(Random.Next(1, 2000));
        }

        private RequestModel(int time)
        {
            _time = time;

            TaskId = _globalId;
            _globalId++;
        }

        public void Execute()
        {
            Thread.Sleep(_time);
        }

        private static readonly Random Random = new Random();

        private static int _globalId;
        private readonly int _time;
    }
}