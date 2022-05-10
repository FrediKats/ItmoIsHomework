using System.Threading;
using System.Threading.Tasks;
using AppliedMath.LoadBalancer.Models;
using AppliedMath.LoadBalancer.Tools;

namespace AppliedMath.LoadBalancer.Services
{
    public class InputStreamService
    {
        public readonly BalancerService Balancer;

        public InputStreamService(LoggerService loggerService)
        {
            Balancer = new BalancerService(loggerService);
        }

        public void Start()
        {
            Balancer.Start();
            Task.Run(Executing);
        }

        private void Executing()
        {
            Balancer.Add(RequestModel.Create());
            Balancer.Add(RequestModel.Create());
            Balancer.Add(RequestModel.Create());
            Balancer.Add(RequestModel.Create());
            while (true)
            {
                Balancer.Add(RequestModel.Create());
                Thread.Sleep(Config.Random.Next(Config.InputStreamMinDelay, Config.InputStreamMaxDelay));
            }
        }
    }
}