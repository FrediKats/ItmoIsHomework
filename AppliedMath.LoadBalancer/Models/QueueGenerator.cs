using System;
using System.Threading;
using System.Threading.Tasks;
using AppliedMath.LoadBalancer.Tools;

namespace AppliedMath.LoadBalancer.Models
{
    public class QueueGenerator
    {
        public readonly LoadBalancer Balancer;

        public QueueGenerator(Logger logger)
        {
            Balancer = new LoadBalancer(logger);
        }

        public void Start()
        {
            Balancer.Start();
            Task.Run(Executing);
        }

        private void Executing()
        {
            while (true)
            {
                Balancer.Add(RequestModel.Create());
                Thread.Sleep(Config.Random.Next(400) + 100);
            }
        }
    }
}