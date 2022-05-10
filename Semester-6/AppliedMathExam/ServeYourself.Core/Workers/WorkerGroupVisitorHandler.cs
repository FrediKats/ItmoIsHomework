using System.Collections.Generic;
using System.Linq;
using ServeYourself.Core.Visitors;

namespace ServeYourself.Core.Workers
{
    public class WorkerGroupVisitorHandler
    {
        public List<IWorker> Workers { get; }

        public WorkerGroupVisitorHandler(List<IWorker> workers)
        {
            Workers = workers;
        }

        public void Handle(Queue<IVisitor> visitors)
        {
            foreach (IWorker worker in Workers)
            {
                if (worker.IsAvailable() && visitors.Count != 0)
                {
                    IVisitor visitor = visitors.Dequeue();
                    worker.AddClient(visitor);
                }
            }
        }

        public void TimeTick(int time)
        {
            foreach (IWorker worker in Workers)
            {
                worker.ApplyTime(time);
            }
        }

        public int ClientInHandling()
        {
            return Workers.Count(w => w.IsAvailable() == false);
        }

        public List<IVisitor> GetHandledVisitors()
        {
            return Workers
                .Where(w => w.IsClientServed())
                .Select(w => w.GetServedClient())
                .ToList();
        }
    }
}