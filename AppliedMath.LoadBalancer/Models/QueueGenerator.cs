using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppliedMath.LoadBalancer.Models
{
    public class QueueGenerator
    {
        private readonly LoadBalancer _balancer;

        public QueueGenerator(Logger logger)
        {
            _balancer = new LoadBalancer(logger);
        }

        public void Start()
        {
            _balancer.Start();
            Task.Run(Executing);
        }

        private void Executing()
        {
            while (true)
            {
                _balancer.Add(RequestModel.Create());
                Thread.Sleep(Tools.Random.Next(400) + 100);
            }
        }
    }
}